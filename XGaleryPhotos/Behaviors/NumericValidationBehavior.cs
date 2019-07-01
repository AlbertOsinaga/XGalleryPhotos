using System;
using Xamarin.Forms;

namespace XGaleryPhotos.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = int.TryParse(args.NewTextValue, out int result);
            if (isValid && (result >= 1 && result <= 20))
                return;
            Entry entry = sender as Entry;
            entry.Text = string.Empty;
        }
    }
}
