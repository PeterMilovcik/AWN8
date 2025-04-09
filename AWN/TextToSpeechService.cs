using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using OpenAI.Audio;

namespace AWN
{
    public class TextToSpeechService
    {
        private readonly AudioClient _audioClient;

        public TextToSpeechService()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("OpenAI API key is not set.");
            }

            _audioClient = new AudioClient("tts-1", apiKey);
        }

        public async Task ConvertTextToSpeechAsync(string text, CancellationToken cancellationToken)
        {
            try
            {
                BinaryData speech = await _audioClient.GenerateSpeechAsync(text, GeneratedSpeechVoice.Onyx);

                if (File.Exists("output.mp3"))
                {
                    File.Delete("output.mp3");
                }

                using (FileStream stream = File.OpenWrite("output.mp3"))
                {
                    speech.ToStream().CopyTo(stream);
                }

                using (var audioFile = new AudioFileReader("output.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            outputDevice.Stop();
                            break;
                        }

                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Text to speech failed.");
            }
        }
    }
}
