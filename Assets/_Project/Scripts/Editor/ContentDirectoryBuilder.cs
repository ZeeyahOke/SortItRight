using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ContentDirectoryBuilder : BuildPlayerProcessor
{
    public const string OutputFolder = "ContentBuild/Scenarios";
    public const string StreamingFolder = "Scenarios";

    private static readonly string[] RootAssets =
    {
        "Assets/_Project/Content/ScenarioContent_Household.asset",
        "Assets/_Project/Content/ScenarioContent_Market.asset"
    };

    [MenuItem("Sort It Right/Build Content Directory")]
    public static void BuildFromMenu()
    {
        Build();
    }

    public static bool Build()
    {
        BuildContentDirectoryParameters parameters = new BuildContentDirectoryParameters
        {
            outputPath = OutputFolder,
            rootAssetPaths = RootAssets,
            options = BuildContentOptions.None
        };

        BuildReport report = BuildPipeline.BuildContentDirectory(parameters);
        bool succeeded = report.summary.result == BuildResult.Succeeded;
        Debug.Log(succeeded ? "Content directory built to " + OutputFolder : "Content directory build failed");
        return succeeded;
    }

    public override void PrepareForBuild(BuildPlayerContext context)
    {
        if (!Build())
        {
            throw new BuildFailedException("Content directory build failed");
        }

        context.AddAdditionalPathToStreamingAssets(OutputFolder, StreamingFolder);
        context.AddPreviousBuildReportDirectory(OutputFolder);
    }
}
