using NAudio.Wave; // Import the NAudio library for audio recording
using System;
using System.Timers; // Import the System.Timers namespace for using Timer
using System.Windows.Forms;

namespace AudioRecordingGUI_V2
{
    public partial class Form1 : Form
    {
        private WaveInEvent? waveSource = null; // Declare a nullable WaveInEvent object for capturing audio input
        private WaveFileWriter? waveFile = null; // Declare a nullable WaveFileWriter object for writing audio to a file
        private System.Timers.Timer? stopTimer; // Declare a nullable Timer object to stop recording after a specified duration
        private string outputFilePath = @"C:\Users\Peter Arias\Documents\VisualStudioFiles\audio.wav"; // Define the output file path
        private System.Windows.Forms.Timer? progressBarTimer; // Timer to update the progress bar
        private int recordingDuration = 8000; // Recording duration in milliseconds

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            StartRecording();
        }

        public void StartRecording()
        {
            waveSource = new WaveInEvent // Initialize the WaveInEvent object
            {
                //mono = 1
                //stereo = 2
                WaveFormat = new WaveFormat(44100, 24, 2) // Set the wave format to 44.1kHz, 24 bits, stereo
            };

            waveSource.DataAvailable += WaveSource_DataAvailable; // Attach the DataAvailable event handler

            waveFile = new WaveFileWriter(outputFilePath, waveSource.WaveFormat); // Initialize the WaveFileWriter with the specified path and format

            waveSource.StartRecording(); // Start recording audio

            // Set up the timer to stop recording after 8 seconds
            stopTimer = new System.Timers.Timer(recordingDuration); // Initialize the timer with an interval of 8000 milliseconds (8 seconds)
            stopTimer.Elapsed += StopTimer_Elapsed; // Attach the Elapsed event handler
            stopTimer.AutoReset = false; // Ensure the timer only fires once
            stopTimer.Start(); // Start the timer

            // Set up the timer to update the progress bar
            progressBar.Value = 0;
            progressBar.Maximum = recordingDuration / 100; // Set the maximum value to match the duration
            progressBarTimer = new System.Windows.Forms.Timer();
            progressBarTimer.Interval = 100; // Update progress bar every 100 milliseconds
            progressBarTimer.Tick += ProgressBarTimer_Tick;
            progressBarTimer.Start();

            recordButton.Enabled = false; // Disable the button to prevent multiple recordings at once

            Console.WriteLine("Recording started. It will stop automatically after 8 seconds."); // Inform the user that recording has started
        }

        private void ProgressBarTimer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value < progressBar.Maximum)
            {
                progressBar.Value += 1;
            }
            else
            {
                progressBarTimer.Stop();
            }
        }

        private void WaveSource_DataAvailable(object? sender, WaveInEventArgs e)
        {
            // Write recorded audio data to the file
            if (waveFile != null) // Check if waveFile is initialized
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded); // Write the audio data to the file
                waveFile.Flush(); // Flush the data to ensure it is written to the file
            }
        }

        private void StopTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            // Stop recording when the timer elapses
            StopRecording(); // Call the method to stop recording
        }

        public void StopRecording()
        {
            // Stop and dispose the wave source
            if (waveSource != null) // Check if waveSource is initialized
            {
                waveSource.StopRecording(); // Stop the recording
                waveSource.Dispose(); // Dispose the waveSource object
                waveSource = null; // Set waveSource to null
            }

            // Dispose the wave file
            if (waveFile != null) // Check if waveFile is initialized
            {
                waveFile.Dispose(); // Dispose the waveFile object
                waveFile = null; // Set waveFile to null
            }

            // Dispose the timer
            if (stopTimer != null) // Check if stopTimer is initialized
            {
                stopTimer.Dispose(); // Dispose the stopTimer object
                stopTimer = null; // Set stopTimer to null
            }

            if (progressBarTimer != null)
            {
                progressBarTimer.Stop();
                progressBarTimer.Dispose();
                progressBarTimer = null;
            }

            recordButton.Invoke((Action)(() => recordButton.Enabled = true)); // Re-enable the record button on the UI thread

            progressBar.Invoke((Action)(() => progressBar.Value = progressBar.Maximum)); // Ensure progress bar is fully filled

            MessageBox.Show($"Recording stopped after 8 seconds.\nThe file has been saved to: {outputFilePath}", "Recording Stopped", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show message box with file path

            progressBar.Invoke((Action)(() => progressBar.Value = 0)); // Reset the progress bar to empty

            Console.WriteLine("Recording stopped after 8 seconds."); // Inform the user that recording has stopped
        }
    }
}
