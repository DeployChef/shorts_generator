using System.Diagnostics;
using FFMpegCore;

int shortTimeSec = 10;

var inputPath = @"D:\\Work\DeployChef\shorts_generator\ShortsGenerator\assets\videos\footage.mp4";
var outputTempPath = @"resultTemp.mp4";
var outputTemp2Path = @"resultTemp2.mp4";
var outputTemp3Path = @"resultTemp3.mp4";

var outputFolder = @"D:\Work\DeployChef\shorts_generator\ShortsGenerator\output";

Console.WriteLine("Get video info");
var mediaInfo = await FFProbe.AnalyseAsync(inputPath);

for (int i = 0; i < 12; i++)
{

    var outputPath = $"result_{Random.Shared.Next(0, 10000)}.mp4";
    var inputAudioPath = $@"D:\\Work\DeployChef\shorts_generator\ShortsGenerator\assets\audio\{i+1}.mp3";

    var startTime = Random.Shared.Next(0, (int)mediaInfo.Duration.TotalSeconds);

if(!Directory.Exists(outputFolder))
    Directory.CreateDirectory(outputFolder);

Console.WriteLine($"Set time {startTime} sec - {startTime + shortTimeSec} sec");
FFMpeg.SubVideo(inputPath,
    outputFolder + "\\" + outputTempPath,
    TimeSpan.FromSeconds(startTime),
    TimeSpan.FromSeconds(startTime + shortTimeSec)
);

Console.WriteLine($"Set music");
FFMpeg.Mute(outputFolder + "\\" + outputTempPath, outputFolder + "\\" + outputTemp2Path);
FFMpeg.ReplaceAudio(outputFolder + "\\" + outputTemp2Path, inputAudioPath, outputFolder + "\\" + outputTemp3Path);

FFMpeg.SubVideo(outputFolder + "\\" + outputTemp3Path,
    outputFolder + "\\" + outputPath,
    TimeSpan.FromSeconds(0),
    TimeSpan.FromSeconds(shortTimeSec)
);

Console.WriteLine($"Done {outputPath}");

//Process.Start("explorer.exe", outputFolder);

Console.WriteLine("DeleteTemp");
File.Delete(outputFolder + "\\" + outputTempPath);
File.Delete(outputFolder + "\\" + outputTemp2Path);
File.Delete(outputFolder + "\\" + outputTemp3Path);
}