namespace KnowledgeBase.Behaviors
{
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;

	/// <summary>
	/// http://stackoverflow.com/questions/7271101/wpf-textbox-textchanged-event-on-programmatic-versus-user-change-of-text-content
	/// </summary>
	public class TextChangedBehavior
	{
		public static readonly DependencyProperty TextChangedCommandProperty =
						DependencyProperty.RegisterAttached("TextChangedCommand",
																								typeof(ICommand),
																								typeof(TextChangedBehavior),
																								new UIPropertyMetadata(TextChangedCommandChanged));

		private static readonly DependencyProperty UserInputProperty =
				DependencyProperty.RegisterAttached("UserInput",
																						typeof(bool),
																						typeof(TextChangedBehavior));

		public static void SetTextChangedCommand(DependencyObject target, ICommand value)
		{
			target.SetValue(TextChangedCommandProperty, value);
		}

		private static void ExecuteTextChangedCommand(TextBox sender, TextChangedEventArgs e)
		{
			e.Handled = SendTextBoxChangedEvent(sender, e);
		}

		private static bool SendTextBoxChangedEvent(TextBox sender, object e)
		{
			var command = (ICommand)sender.GetValue(TextChangedCommandProperty);
			var arguments = new object[]
			                {
												sender,
												e,
												TextChangedBehavior.GetUserInput(sender),
												sender.Text
											};

			// Check whether this attached behaviour is bound to a RoutedCommand
			if (command is RoutedCommand)
			{
				// Execute the routed command
				(command as RoutedCommand).Execute(arguments, sender);
				return true;
			}
			else
			{
				// Execute the Command as bound delegate
				command.Execute(arguments);
				return true;
			}

			//// return false;
		}

		private static bool GetUserInput(DependencyObject target)
		{
			return (bool)target.GetValue(UserInputProperty);
		}

		private static void SetUserInput(DependencyObject target, bool value)
		{
			target.SetValue(UserInputProperty, value);
		}

		private static void TextBoxOnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (e.Command != ApplicationCommands.Cut)
			{
				return;
			}

			var textBox = sender as TextBox;
			if (textBox == null)
			{
				return;
			}

			SetUserInput(textBox, true);
		}

		private static void TextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			var textBox = (TextBox)sender;

			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
			{
				switch (e.Key)
				{
					case Key.Left:
					case Key.Right:
						SetUserInput(textBox, true);
						SendTextBoxChangedEvent(sender as TextBox, e);
					break;

					default:
						return;
				}

				return;
			}

			switch (e.Key)
			{
				case Key.Return:
					////if (textBox.AcceptsReturn)
					////{
						SetUserInput(textBox, true);
						SendTextBoxChangedEvent(sender as TextBox, e);
						////}
					break;

				case Key.Delete:
					if (textBox.SelectionLength > 0 || textBox.SelectionStart < textBox.Text.Length)
					{
						SetUserInput(textBox, true);
					}
					break;

				case Key.Back:
					if (textBox.SelectionLength > 0 || textBox.SelectionStart > 0)
					{
						SetUserInput(textBox, true);
					}
					break;
			}
		}

		private static void TextBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			SetUserInput((TextBox)sender, true);
		}

		private static void TextBoxOnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = (TextBox)sender;
			ExecuteTextChangedCommand(textBox, e);
			SetUserInput(textBox, false);
		}

		private static void TextBoxOnTextPasted(object sender, DataObjectPastingEventArgs e)
		{
			var textBox = (TextBox)sender;
			if (e.SourceDataObject.GetDataPresent(DataFormats.Text, true) == false)
			{
				return;
			}

			SetUserInput(textBox, true);
		}

		private static void TextChangedCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			var textBox = target as TextBox;
			if (textBox == null)
			{
				return;
			}

			if (e.OldValue != null)
			{
				textBox.PreviewKeyDown -= TextBoxOnPreviewKeyDown;
				textBox.PreviewTextInput -= TextBoxOnPreviewTextInput;
				CommandManager.RemovePreviewExecutedHandler(textBox, TextBoxOnPreviewExecuted);
				DataObject.RemovePastingHandler(textBox, TextBoxOnTextPasted);
				textBox.TextChanged -= TextBoxOnTextChanged;
			}

			if (e.NewValue != null)
			{
				textBox.PreviewKeyDown += TextBoxOnPreviewKeyDown;
				textBox.PreviewTextInput += TextBoxOnPreviewTextInput;
				CommandManager.AddPreviewExecutedHandler(textBox, TextBoxOnPreviewExecuted);
				DataObject.AddPastingHandler(textBox, TextBoxOnTextPasted);
				textBox.TextChanged += TextBoxOnTextChanged;
			}
		}
	}
}
