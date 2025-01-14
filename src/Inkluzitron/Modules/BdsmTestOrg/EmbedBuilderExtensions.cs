﻿using Discord;
using Inkluzitron.Data.Entities;
using Inkluzitron.Extensions;
using Inkluzitron.Models.Settings;
using System;
using System.Linq;

namespace Inkluzitron.Modules.BdsmTestOrg
{
    static public class EmbedBuilderExtensions
    {
        static public EmbedBuilder WithBdsmTestOrgQuizInvitation(this EmbedBuilder builder, BdsmTestOrgSettings settings, IUser user)
        {
            builder.WithTitle(settings.TestLinkUrl);
            builder.WithColor(new Color(0, 0, 0));
            builder.WithUrl(settings.TestLinkUrl);
            builder.WithDescription(settings.NoResultsOnRecordMessage);
            builder.WithFooter("0/0");
            builder.WithMetadata(new QuizEmbedMetadata
            {
                ResultId = 0,
                UserId = user.Id,
                PageNumber = 0
            });

            return builder;
        }

        static public EmbedBuilder WithBdsmTestOrgQuizResult(this EmbedBuilder builder, BdsmTestOrgSettings settings, BdsmTestOrgResult quizResult, int pageNumber, int pageCount)
        {
            if (quizResult is null)
                throw new ArgumentNullException(nameof(quizResult));

            builder.WithTitle(quizResult.Link);
            builder.WithColor(new Color(0, 0, 0));
            builder.WithUrl(quizResult.Link);
            builder.WithTimestamp(quizResult.SubmittedAt);
            builder.WithFooter($"{pageNumber}/{pageCount}");
            builder.WithMetadata(new QuizEmbedMetadata
            {
                ResultId = quizResult.Id,
                UserId = quizResult.UserId,
                PageNumber = pageNumber
            });

            var relevantItems = quizResult.Items
                .Where(i => i.Score >= settings.StrongTraitThreshold)
                .OrderByDescending(i => i.Score)
                .ToList();

            if (relevantItems.Count == 0)
                builder.WithDescription(settings.NoTraitsToReportMessage);

            foreach (var relevantItem in relevantItems)
                builder.AddField(relevantItem.Trait.GetDisplayName(), $"{relevantItem.Score:P0}");

            return builder;
        }
    }
}
