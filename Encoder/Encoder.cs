using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Encoder
{
	public class EncoderProcessor
	{
		public string Encode( string message )
		{
			message = EncodeNumbers( message );

			return EncodeLetters( message );
		}

		private string EncodeLetters( string message )
		{
			StringBuilder encodedMessage = new StringBuilder( );
			foreach ( char c in message )
			{
				if ( !char.IsLetter( c ) && !char.IsWhiteSpace( c ) )
				{
					encodedMessage.Append( c );
					continue;
				}
				var letter = char.ToLower( c );
				if ( letter.IsVowel( out int encodedChar ) )
				{
					encodedMessage.Append( encodedChar );
				}
				else if ( letter == 'y' )
				{
					encodedMessage.Append( ' ' );
				}
				else if ( letter == ' ' )
				{
					encodedMessage.Append( 'y' );
				}
				else
				{
					encodedMessage.Append( (char)( letter - 1 ) );
				}
			}
			return encodedMessage.ToString( );
		}

		private string EncodeNumbers( string message )
		{
			Regex regex = new Regex( "[0-9]+" );
			MatchCollection matches = regex.Matches( message );
			foreach ( Match match in matches )
			{
				string matchedString = match.ToString( );
				message = message.Replace( matchedString, matchedString.Reverse( ) );
			}
			return message;
		}
	}

	public static class Extensions
	{
		//extension method for string reversal.
		public static string Reverse( this string value )
		{
			char[ ] array = value.ToCharArray( );
			Array.Reverse( array );
			return new string( array );
		}

		//check vowels and out parameter corresponding encoded char
		public static bool IsVowel( this char c, out int encodedChar )
		{
			string vowels = "aeiou";
			encodedChar = -1;

			int index = vowels.IndexOf( c );
			if ( index > -1 )
			{
				encodedChar = index + 1;
				return true;
			}
			return false;
		}
	}
}