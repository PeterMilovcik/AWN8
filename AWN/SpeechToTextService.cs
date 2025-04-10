using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using OpenAI.Audio;

namespace AWN
{
    public class SpeechToTextService
    {
        private readonly AudioClient _audioClient;

        public SpeechToTextService()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            _audioClient = new AudioClient("whisper-1", apiKey);
        }

        public async Task<string> TranscribeAudioAsync(string audioFilePath)
        {
            try
            {
                AudioTranscriptionOptions options = new AudioTranscriptionOptions
                {
                    Language = "en"
                };

                AudioTranscription transcription = await _audioClient.TranscribeAudioAsync(audioFilePath, options);
                return transcription.Text;
            }
            catch (Exception ex)
            {
                return $"Error transcribing audio: {ex.Message}";
            }
        }

        public void CaptureAudio(string outputFilePath)
        {
            using (var waveIn = new WaveInEvent())
            {
                waveIn.WaveFormat = new WaveFormat(44100, 16, 1);
                using (var writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat))
                {
                    waveIn.DataAvailable += (sender, e) =>
                    {
                        writer.Write(e.Buffer, 0, e.BytesRecorded);
                    };

                    waveIn.RecordingStopped += (sender, e) =>
                    {
                        writer.Dispose();
                    };

                    waveIn.StartRecording();
                    Console.WriteLine("Recording... Press Enter to stop.");
                    Console.ReadLine();
                    waveIn.StopRecording();
                    waveIn.Dispose();
                }
            }
        }
    }
}
