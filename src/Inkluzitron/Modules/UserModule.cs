﻿using Discord;
using Discord.Commands;
using Inkluzitron.Data;
using Inkluzitron.Enums;
using Inkluzitron.Extensions;
using Inkluzitron.Models.Settings;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Inkluzitron.Modules
{
    [Name("Nastavení uživatele")]
    [Summary("Nastavení oslovení se používá pro správný výpis hlášek bota. Oslovení je možné nastavit pomocí příkazů níže, nebo se nastaví automaticky po vložení BDSM testu.")]
    public class UserModule : ModuleBase
    {
        private ReactionSettings ReactionSettings { get; }
        private IConfiguration Configuration { get; }
        private DatabaseFactory DatabaseFactory { get; }
        private BotDatabaseContext DbContext { get; set; }

        public UserModule(IConfiguration configuration, ReactionSettings reactionSettings,
            DatabaseFactory databaseFactory)
        {
            Configuration = configuration;
            ReactionSettings = reactionSettings;
            DatabaseFactory = databaseFactory;
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            DbContext = DatabaseFactory.Create();
            base.BeforeExecute(command);
        }

        protected override void AfterExecute(CommandInfo command)
        {
            DbContext?.Dispose();
            base.AfterExecute(command);
        }

        [Command("pronouns")]
        [Alias("osloveni")]
        [Summary("Vypíše svoje preferované oslovení nebo oslovení vybraného uživatele.")]
        public async Task ShowGenderAsync([Name("uživatel")]IUser user = null)
        {
            var genderMsg = Configuration["UserModule:UserPronounsMessage"];
            var notFoundMsg = Configuration["UserModule:UserNotFoundMessage"];

            if (user == null) user = Context.User;

            if (user.IsBot)
            {
                if(user.Id == Context.Client.CurrentUser.Id)
                {
                    await ReplyAsync(string.Format(genderMsg, user.GetDisplayName(), Configuration["UserModule:UserPronounsBotSelf"]));
                    return;
                }

                await ReplyAsync(string.Format(genderMsg, user.GetDisplayName(), "je bot a nemá preferované oslovení."));
                return;
            }

            var userDb = await DbContext.GetUserEntityAsync(user);
            if (userDb == null)
            {
                await ReplyAsync(string.Format(notFoundMsg, user.GetDisplayName()));
                return;
            }

            var gender = userDb.Gender == Gender.Unspecified ?
                "nemá preferované oslovení." :
                $"je {userDb.Gender.GetDisplayName()}.";

            await ReplyAsync(string.Format(genderMsg, user.GetDisplayName(), gender));
        }

        [Command("pronouns set he")]
        [Alias("pronouns set him", "pronouns set on", "osloveni set he", "osloveni set him", "osloveni set on")]
        [Summary("Nastaví svoje preferované oslovení jako mužské - he/him, on.")]
        public Task SetGenderMaleAsync()
            => SetGenderAsync(Gender.Male);

        [Command("pronouns set she")]
        [Alias("pronouns set her", "pronouns set ona", "osloveni set she", "osloveni set her", "osloveni set ona")]
        [Summary("Nastaví svoje preferované oslovení jako ženské - she/her, ona.")]
        public Task SetGenderFemaleAsync()
            => SetGenderAsync(Gender.Female);

        [Command("pronouns unset")]
        [Alias("pronouns set other", "osloveni unset", "osloveni set other")]
        [Summary("Nastaví neutrální oslovení.")]
        public Task UnsetGenderAsync()
            => SetGenderAsync(Gender.Unspecified);

        public async Task SetGenderAsync(Gender gender)
        {
            var user = await DbContext.GetOrCreateUserEntityAsync(Context.User);
            user.Gender = gender;
            await DbContext.SaveChangesAsync();
            await Context.Message.AddReactionAsync(ReactionSettings.Checkmark);
        }
    }
}
