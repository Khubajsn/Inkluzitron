﻿using Discord;
using Inkluzitron.Enums;
using Inkluzitron.Extensions;
using Inkluzitron.Models.Settings;
using System;

namespace Inkluzitron.Models
{
    public class BdsmTraitOperationCheck
    {
        private readonly BdsmTraitOperationCheckTranslations _translations;
        private readonly Gender _userGender;
        private readonly Gender _targetGender;

        public BdsmTraitOperationCheck(BdsmTraitOperationCheckTranslations translations,
            Gender userGender, Gender targetGender)
        {
            _translations = translations ?? throw new ArgumentNullException(nameof(translations));
            _userGender = userGender;
            _targetGender = targetGender;
        }

        public IUser User { get; set; }
        public IUser Target { get; set; }
        public BdsmTraitOperationCheckResult Result { get; set; }
        public bool IsSuccessful => Result != BdsmTraitOperationCheckResult.RollFailed;

        public int UserSubmissiveness { get; set; }
        public int UserDominance { get; set; }
        public int TargetSubmissiveness { get; set; }
        public int TargetDominance { get; set; }

        public int RolledValue { get; set; }
        public int RollMaximum { get; set; }
        public int RequiredValue { get; set; }
        public int SubstractedPoints => IsSuccessful ? 0 : RequiredValue - RolledValue;

        public override string ToString()
        {
            string format;

            switch (Result)
            {
                case BdsmTraitOperationCheckResult.UserHasNoTest:
                    return string.Format(_translations.MissingTest, User.Username);

                case BdsmTraitOperationCheckResult.TargetHasNoTest:
                    return string.Format(_translations.MissingTest, Target.Username);

                case BdsmTraitOperationCheckResult.Self:
                    return string.Format(_translations.Self, User.Username);

                case BdsmTraitOperationCheckResult.InCompliance:
                    return string.Format(
                        _translations.InCompliance,
                        User.Username, UserSubmissiveness, UserDominance,
                        Target.Username, TargetSubmissiveness, TargetDominance
                    );

                case BdsmTraitOperationCheckResult.RollSucceeded:
                    format = _translations.RollSucceeded;
                    break;

                case BdsmTraitOperationCheckResult.RollFailed:
                    format = _translations.RollFailed;
                    break;

                default:
                    return base.ToString();
            }

            return string.Format(
                format,
                User.GetDisplayName(true), UserSubmissiveness, UserDominance,
                Target.GetDisplayName(true), TargetSubmissiveness, TargetDominance,
                RolledValue, RollMaximum, RequiredValue, SubstractedPoints,
                _translations.RollFailedLossGendered[(int)_userGender],
                _translations.RollFailedGainGendered[(int)_targetGender]
            );
        }
    }
}
