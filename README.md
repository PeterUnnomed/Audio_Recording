# AudioRecordingGUI_V2

AudioRecordingGUI_V2 is a Windows Forms application in C# that allows you to record audio from the microphone and save the file in WAV format. The interface includes a record button, a progress bar and a popup message with the file location at the end of the recording.

## Features

- Record audio in WAV format.
- Easy to use record button.
- Recording progress bar.
- Pop-up message with the location of the saved file.
- Window centered on screen at startup.

  ![Application Screenshot](images/screenshot.png)

## Requirements

- .NET Framework (e.g., .NET Framework 4.8)
- Library [NAudio] (https://github.com/naudio/NAudio)

1. Clone the repository:
   ````bash
   git clone https://github.com/PeterUnnomed/Audio_Recording.git
2. Open the project in Visual Studio. 
3. Restore the NuGet dependencies (NAudio).
4. Compile and run the project.

## Usage
1. Run the application.
2. Click "Record" to start recording.
3. The progress bar will show the remaining time (8 seconds).
4. When finished, a message with the location of the saved file will be displayed.
5. The progress bar will reset.

## Contributions
Contributions are welcome. Open issues and pull requests to improve the project.

## Credits
Uses the NAudio library for audio handling in C#.

## Developed by 
Peter Arias 
