using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Services
{
	public class SpeechService
	{
		static string SubscriptionKey = "1ef2318152104fecbc3e4f1338765260";
		static string ServiceRegion = "westeurope";

		static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
		{
			switch (speechRecognitionResult.Reason)
			{
				case ResultReason.RecognizedSpeech:
					Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
					break;
				case ResultReason.NoMatch:
					Console.WriteLine($"NOMATCH: Speech could not be recognized.");
					break;
				case ResultReason.Canceled:
					var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
					Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

					if (cancellation.Reason == CancellationReason.Error)
					{
						Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
						Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
						Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
					}
					break;
			}
		}

		async static Task Main(string[] args)
		{
			var speechConfig = SpeechConfig.FromSubscription(SubscriptionKey, ServiceRegion);
			speechConfig.SpeechRecognitionLanguage = "es-ES";

			//using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
			//using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

			Console.WriteLine("Speak into your microphone.");
			//var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
			//OutputSpeechRecognitionResult(speechRecognitionResult);
		}
	}
}
