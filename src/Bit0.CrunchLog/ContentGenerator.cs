﻿using System.Linq;
using Bit0.CrunchLog.Config;
using Bit0.CrunchLog.Extensions;
using Bit0.CrunchLog.ThemeHandler;
using Microsoft.Extensions.Logging;

namespace Bit0.CrunchLog
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ContentGenerator : IContentGenerator
    {
        private readonly IContentProvider _contentProvider;
        private readonly IThemeHandler _themeHandler;
        private readonly CrunchConfig _config;
        private readonly ILogger<ContentGenerator> _logger;

        public ContentGenerator(IContentProvider contentProvider,
            IThemeHandler themeHandler,
            CrunchConfig config,
            ILogger<ContentGenerator> logger)
        {
            _themeHandler = themeHandler;
            _logger = logger;
            _config = config;
            _contentProvider = contentProvider;
        }

        public void CleanOutput()
        {
            if (_config.Paths.OutputPath.Exists)
            {
                _config.Paths.OutputPath.Delete(true);
            }

            _logger.LogInformation($"Cleaned output folder {_config.Paths.OutputPath.FullName}");

        }

        public void PublishCategories()
        {
        //    var pages = _contentProvider.PostCategories.SelectMany(a => a.Pages).ToList();

        //    foreach (var page in pages)
        //    {
        //        //category.WriteFile(_config.Paths.OutputPath);
        //    }

        //    _logger.LogInformation($"Categories published in {pages.Count} pages");
        }

        public void PublishTags()
        {
            //var pages = _contentProvider.PostTags.SelectMany(a => a.Pages).ToList();

            //foreach (var page in pages)
            //{
            //    //tag.WriteFile(_config.Paths.OutputPath);
            //}

            //_logger.LogInformation($"Tags published in {pages.Count} pages");
        }

        public void PublishArchive()
        {
            //var pages = _contentProvider.PostArchives.SelectMany(a => a.Pages).ToList();

            //foreach (var page in pages)
            //{
            //    //_themeHandler.WriteFile(archive);
            //}

            //_logger.LogInformation($"Archives published in {pages.Count} pages");
        }

        public void PublishHome()
        {
            //var pages = _contentProvider.Home.Pages.ToList();

            //foreach (var page in pages)
            //{
            //    _themeHandler.WriteFile(page);
            //}

            //_logger.LogInformation($"Home published in {pages.Count} pages");
        }

        public void PublishContent()
        {
            var published = _contentProvider.PublishedContent.ToList();

            foreach (var content in published)
            {
                _themeHandler.WriteFile(content.Post);
            }

            _logger.LogInformation($"Published {published.Count} posts/pages");
        }

        public void PublishImages()
        {
            _config.Paths.ImagesPath.Copy(_config.Paths.OutputPath.CombineDirPath("images"));
        }

        public void Publish()
        {
            // [x] create posts pages
            // [x] create main index
            // [ ] create archive pages
            // [ ] create tag pages
            // [ ] create category pages
            // [ ] pagination

            // [ ] get parent for pages
            // [ ] create a tree
            // [ ] generate permalink from tree

            _themeHandler.InitOutput();
                    
            PublishImages();
            PublishHome();
            PublishContent();
            PublishArchive();
            PublishCategories();
            PublishTags();
        }
    }
}
