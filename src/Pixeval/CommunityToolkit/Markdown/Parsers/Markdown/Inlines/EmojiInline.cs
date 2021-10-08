﻿using System.Collections.Generic;
using Pixeval.CommunityToolkit.Markdown.Parsers.Core;
using Pixeval.CommunityToolkit.Markdown.Parsers.Markdown.Enums;
using Pixeval.CommunityToolkit.Markdown.Parsers.Markdown.Helpers;

namespace Pixeval.CommunityToolkit.Markdown.Parsers.Markdown.Inlines
{
    /// <summary>
    /// Represents a span containing emoji symbol.
    /// </summary>
    public partial class EmojiInline : MarkdownInline, IInlineLeaf
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmojiInline"/> class.
        /// </summary>
        public EmojiInline()
            : base(MarkdownInlineType.Emoji)
        {
        }

        /// <summary>
        /// Returns the chars that if found means we might have a match.
        /// </summary>
        internal static void AddTripChars(List<InlineTripCharHelper> tripCharHelpers)
        {
            tripCharHelpers.Add(new InlineTripCharHelper() { FirstChar = ':', Method = InlineParseMethod.Emoji });
        }

        internal static InlineParseResult? Parse(string markdown, int start, int maxEnd)
        {
            if (start >= maxEnd - 1)
            {
                return null;
            }

            // Check the start sequence.
            var startSequence = markdown.Substring(start, 1);
            if (startSequence != ":")
            {
                return null;
            }

            // Find the end of the span.
            var innerStart = start + 1;
            var innerEnd = Common.IndexOf(markdown, startSequence, innerStart, maxEnd);
            if (innerEnd == -1)
            {
                return null;
            }

            // The span must contain at least one character.
            if (innerStart == innerEnd)
            {
                return null;
            }

            // The first character inside the span must NOT be a space.
            if (ParseHelpers.IsMarkdownWhiteSpace(markdown[innerStart]))
            {
                return null;
            }

            // The last character inside the span must NOT be a space.
            if (ParseHelpers.IsMarkdownWhiteSpace(markdown[innerEnd - 1]))
            {
                return null;
            }

            var emojiName = markdown.Substring(innerStart, innerEnd - innerStart);

            if (_emojiCodesDictionary.TryGetValue(emojiName, out var emojiCode))
            {
                var result = new EmojiInline { Text = char.ConvertFromUtf32(emojiCode), Type = MarkdownInlineType.Emoji };
                return new InlineParseResult(result, start, innerEnd + 1);
            }

            return null;
        }

        /// <inheritdoc/>
        public string? Text { get; set; }
    }
}