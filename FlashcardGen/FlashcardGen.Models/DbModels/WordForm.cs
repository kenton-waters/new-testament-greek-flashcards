﻿using Microsoft.EntityFrameworkCore;

namespace FlashcardGen.Models.DbModels
{
    [Index(nameof(LowercaseSpelling), nameof(RobinsonsMorphologicalAnalysisCode), nameof(LexemeId))]
    public class WordForm
    {
        public int WordFormId { get; set; }

        public string LowercaseSpelling { get; set; }
        public string RobinsonsMorphologicalAnalysisCode { get; set; } //Grammatical form

        public int LexemeId { get; set; }
        public virtual Lexeme Lexeme { get; set; }
        public virtual ICollection<WordFormOccurrence> WordFormOccurrences { get; set; }
    }
}
