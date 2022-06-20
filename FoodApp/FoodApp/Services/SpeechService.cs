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
		private const string SubscriptionKey = "1ef2318152104fecbc3e4f1338765260";
		private const string ServiceRegion = "westeurope";

		public const string MaleVoice = "es-ES-AlvaroNeural";
		public const string FemaleVoice = "es-ES-ElviraNeural";

		public string Voice;




		public async Task TextToSpeech(string text)
		{
			var speechConfig = SpeechConfig.FromSubscription(SubscriptionKey, ServiceRegion);
			speechConfig.SpeechSynthesisVoiceName = Voice;

			using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
			{
				var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
			}
		}

		public async Task<SpeechRecognitionResult> SpeechToText()
		{
			var speechConfig = SpeechConfig.FromSubscription(SubscriptionKey, ServiceRegion);
			speechConfig.SpeechRecognitionLanguage = "es-ES";

			var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
			SpeechRecognizer speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

			return await speechRecognizer.RecognizeOnceAsync();

			//speechRecognizer.Recognized += Interpreter;

			//await TextToSpeech(speechRecognitionResult.Text);
			//OutputSpeechRecognitionResult(speechRecognitionResult);

		}

		private static void Interpreter(object sender, RecognitionEventArgs e)
		{
			

			throw new NotImplementedException();
		}
	}
}
