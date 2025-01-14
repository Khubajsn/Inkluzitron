﻿using Inkluzitron.Extensions;
using Microsoft.Extensions.Configuration;

namespace Inkluzitron.Models.Settings
{
    public class BdsmTestOrgSettings
    {
        public BdsmTestOrgApiKey ApiKey { get; }
        public string NoResultsOnRecordMessage { get; }
        public string InvalidFormatMessage { get; }
        public string LinkAlreadyPresentMessage { get; }
        public string InvalidTraitMessage { get; }
        public string InvalidPercentageMessage { get; }
        public string InvalidTraitCountMessage { get; }
        public string NoMatchesMessage { get; }
        public int MaximumMatchCount { get; }
        public double StrongTraitThreshold { get; }
        public double WeakTraitThreshold { get; }
        public string NoTraitsToReportMessage { get; }
        public string InvalidVerboseModeUsage { get; }
        public string BadFilterQueryMessage { get; }
        public string TestLinkUrl { get; }
        public string NoContentToStats { get; }

        public BdsmTestOrgSettings(IConfiguration config)
        {
            var cfg = config.GetSection("BdsmTestOrgQuizModule");
            cfg.AssertExists();

            ApiKey = cfg.GetRequired<BdsmTestOrgApiKey>(nameof(ApiKey));
            NoResultsOnRecordMessage = cfg.GetRequired<string>(nameof(NoResultsOnRecordMessage));
            InvalidFormatMessage = cfg.GetRequired<string>(nameof(InvalidFormatMessage));
            LinkAlreadyPresentMessage = cfg.GetRequired<string>(nameof(LinkAlreadyPresentMessage));
            InvalidTraitMessage = cfg.GetRequired<string>(nameof(InvalidTraitMessage));
            InvalidPercentageMessage = cfg.GetRequired<string>(nameof(InvalidPercentageMessage));
            InvalidTraitCountMessage = cfg.GetRequired<string>(nameof(InvalidTraitCountMessage));
            NoMatchesMessage = cfg.GetRequired<string>(nameof(NoMatchesMessage));
            MaximumMatchCount = cfg.GetRequired<int>(nameof(MaximumMatchCount));
            StrongTraitThreshold = cfg.GetRequired<double>(nameof(StrongTraitThreshold));
            WeakTraitThreshold = cfg.GetRequired<double>(nameof(WeakTraitThreshold));
            NoTraitsToReportMessage = cfg.GetRequired<string>(nameof(NoTraitsToReportMessage));
            BadFilterQueryMessage = cfg.GetRequired<string>(nameof(BadFilterQueryMessage));
            TestLinkUrl = cfg.GetRequired<string>(nameof(TestLinkUrl));
            NoContentToStats = cfg.GetRequired<string>(nameof(NoContentToStats));
        }
    }
}
