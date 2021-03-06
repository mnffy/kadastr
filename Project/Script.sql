        private List<Helpers.Tuple<short>> GetHighlight(short configId, long eventId, short? parentConfigId)
        {

            //Has default language active in current current config
            var hasDefLngInCurrentConfig = configId == parentConfigId || //always true for parent config
                                           _ctx.LanguageActive
                                           .Where(x => x.LanguageId == SportsBookEntities.DEFAULT_LANGUAGE_ID && x.TemplateId == configId)
                                           .Any();

            //Is event hl in default lng
            var isHighlightDefaultLang =
                _ctx.EventHighlights.Any(
                    eventHighlight => eventHighlight.EventId == eventId
                          && (
                              eventHighlight.TemplateId == configId
                              ||
                              //We check parent only when dont have current default lng
                              eventHighlight.TemplateId == parentConfigId && !hasDefLngInCurrentConfig
                              )
                          && eventHighlight.LanguageId == SportsBookEntities.DEFAULT_LANGUAGE_ID);

            ///All active languages where hl is true and false for event
            var highLightsByActiveLangs = from langActive in _ctx.LanguageActive
                                          join evHighlights in (from eventHighlight in _ctx.EventHighlights
                                                                where eventHighlight.EventId == eventId
                                                                &&
                                                                (
                                                                eventHighlight.TemplateId == configId
                                                                ||
                                                                //We check parent only when dont have current default lng
                                                                eventHighlight.TemplateId == parentConfigId && !hasDefLngInCurrentConfig
                                                                )
                                                                select eventHighlight) on new { langActive.LanguageId } equals new { evHighlights.LanguageId } into gj
                                          from eh in gj.DefaultIfEmpty()
                                          where (langActive.TemplateId == configId || langActive.TemplateId == parentConfigId) &&
                                                langActive.LngUsageType == LanguagePageEnum.TemplateEvents
                                          select new { langActive.LanguageId, IsHighLight = eh != null };

            //Languages onlt where hl is true for events
            var hl = from highLight in highLightsByActiveLangs
                     where highLight.IsHighLight
                     select highLight.LanguageId;

            //Union with languages from default
            if (isHighlightDefaultLang)
            {
                hl = hl.Union(_ctx.Languages.Select(lang => lang.LanguageId)
                    //Dont add languages which is active but with false hl for event
                    .Except(from highLightByLang in highLightsByActiveLangs select highLightByLang.LanguageId));
            }

            //Not need to show def lng in hl. Only real languages needed.
            hl = hl.Where(x => x != SportsBookEntities.DEFAULT_LANGUAGE_ID);

            return hl.Select(x => new Helpers.Tuple<short> { Item1 = x }).ToList();
        }
