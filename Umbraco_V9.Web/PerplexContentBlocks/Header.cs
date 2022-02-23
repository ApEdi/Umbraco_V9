using Perplex.ContentBlocks.Categories;
using Perplex.ContentBlocks.Definitions;
using Perplex.ContentBlocks.Presets;
using System;
using System.Linq;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using static Umbraco.Cms.Core.Constants;

namespace Umbraco_V9.Web.PerplexContentBlocks
{
    public class HeaderComposer : ComponentComposer<HeaderComponent> { }

    public class HeaderComponent : IComponent
    {
        private readonly IContentBlockDefinitionRepository _definitions;
        private readonly PropertyEditorCollection _propertyEditors;
        private readonly IDataTypeService _dataTypeService;
        private readonly IContentTypeService _contentTypeService;
        private readonly IContentBlockCategoryRepository _categoryRepository;
        private readonly IContentBlocksPresetRepository _presetRepository;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IConfigurationEditorJsonSerializer _configurationEditorJsonSerializer;
        private readonly IRuntimeState _runtimeState;

        public HeaderComponent(
            IContentBlockDefinitionRepository definitions,
            PropertyEditorCollection propertyEditors,
            IDataTypeService dataTypeService,
            IContentTypeService contentTypeService,
            IContentBlockCategoryRepository categoryRepository,
            IContentBlocksPresetRepository presetRepository,
            IShortStringHelper shortStringHelper,
            IConfigurationEditorJsonSerializer configurationEditorJsonSerializer,
            IRuntimeState runtimeState)
        {
            _definitions = definitions;
            _propertyEditors = propertyEditors;
            _dataTypeService = dataTypeService;
            _contentTypeService = contentTypeService;
            _categoryRepository = categoryRepository;
            _presetRepository = presetRepository;
            _shortStringHelper = shortStringHelper;
            _configurationEditorJsonSerializer = configurationEditorJsonSerializer;
            _runtimeState = runtimeState;
        }

        public void Initialize()
        {
            if (_runtimeState.Level != Umbraco.Cms.Core.RuntimeLevel.Run)
            {
                return;
            }

            Guid dataTypeKey = new Guid("857c0545-966c-4272-b26b-ea29d530af41");
            CreateExampleBlock("headerBlock", dataTypeKey);

            var specialCategoryId = new Guid("0e3f7336-6fd1-49a6-9561-7e476cac88d7");
            _categoryRepository.Add(new ContentBlockCategory
            {
                Id = specialCategoryId,
                Name = "Specials",
                Icon = "/Media/icons.svg#icon-cat-special"
            });

            // Block
            var block = new ContentBlockDefinition
            {
                Name = "Block",
                Id = new Guid("62e53927-5897-4139-9dd5-0adc0a644c68"),
                DataTypeKey = dataTypeKey,
                // PreviewImage will usually be a path to some image,
                // for this demo we use a base64-encoded PNG of 3x2 pixels
                PreviewImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAMAAAACCAYAAACddGYaAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAATSURBVAiZYzzDwPCfAQqYGJAAACokAc/b6i7NAAAAAElFTkSuQmCC",
                Description = "Block",

                Layouts = new IContentBlockLayout[]
                    {
                        new ContentBlockLayout
                        {
                            Id = new Guid("8fc459b5-e12c-4e0e-86ad-4cc2090f051a"),
                            Name = "Red",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae interdum dolor, sit amet luctus odio.",
                            PreviewImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAMAAAACCAYAAACddGYaAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAATSURBVAiZYzzDwPCfAQqYGJAAACokAc/b6i7NAAAAAElFTkSuQmCC",
                            ViewPath = "~/Views/Partials/ExampleBlock/Red.cshtml"
                        },
                        new ContentBlockLayout
                        {
                            Id = new Guid("c7269b0f-5728-4876-853d-84e951967ead"),
                            Name = "Green",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae interdum dolor, sit amet luctus odio. Nam laoreet at odio eu faucibus. Vivamus non rhoncus erat, sit amet efficitur ipsum.",
                            PreviewImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAMAAAACCAYAAACddGYaAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAATSURBVAiZYyy+JPafAQqYGJAAADcdAl5UlCmyAAAAAElFTkSuQmCC",
                            ViewPath = "~/Views/Partials/ExampleBlock/Green.cshtml"
                        },
                        new ContentBlockLayout
                        {
                            Id = new Guid("133f4e72-a904-4e5f-b288-b27ec8e0c44a"),
                            Name = "Blue",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae interdum dolor, sit amet luctus odio. Nam laoreet at odio eu faucibus. Vivamus non rhoncus erat, sit amet efficitur ipsum. Pellentesque tempus tellus eu posuere varius. Nulla elementum lacus lacus. Curabitur elementum faucibus velit sed mollis.",
                            PreviewImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAMAAAACCAYAAACddGYaAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAATSURBVAiZYzRJXfKfAQqYGJAAADOAAkAWXApqAAAAAElFTkSuQmCC",
                            ViewPath = "~/Views/Partials/ExampleBlock/Blue.cshtml",
                        },
                    },

                CategoryIds = new[]
                {
                    Perplex.ContentBlocks.Constants.Categories.Content,
                    specialCategoryId,
                }
            };

            // Header
            var header = new ContentBlockDefinition
            {
                Name = "Header",
                Id = new Guid("8e4bb4a7-013b-453a-ad05-38c60e1b8e77"),
                DataTypeKey = dataTypeKey,
                PreviewImage = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAMAAAACCAYAAACddGYaAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAOxAAADsQBlSsOGwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAATSURBVAiZY/zz0v8/AxQwMSABAEvFAzckGfK1AAAAAElFTkSuQmCC",
                Description = "Header Block",

                Layouts = new IContentBlockLayout[]
                    {
                        new ContentBlockLayout
                        {
                            Id = new Guid("6149b596-35ff-4fe4-a096-603ac750dd71"),
                            Name = "Layout 1",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae interdum dolor, sit amet luctus odio.",
                            PreviewImage = "/assets/img/header-layout-1.png",
                            ViewPath = "~/Views/Partials/Header/layoutone.cshtml"
                        },
                        new ContentBlockLayout
                        {
                            Id = new Guid("f16f95af-495b-43e5-a76e-d9027d95ce6f"),
                            Name = "Layout 2",
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec vitae interdum dolor, sit amet luctus odio. Nam laoreet at odio eu faucibus. Vivamus non rhoncus erat, sit amet efficitur ipsum.",
                            PreviewImage = "/assets/img/header-layout-2.png",
                            ViewPath = "~/Views/Partials/Header/layouttwo.cshtml"
                        },
                    },

                CategoryIds = new[]
                {
                    Perplex.ContentBlocks.Constants.Categories.Headers,
                }
            };

            _definitions.Add(block);
            _definitions.Add(header);

            //var all = _definitions.GetAll().ToList();

            //for (int i = 0; i < all.Count; i++)
            //{
            //    var def = all[i];

            //    for (int j = 0; j < 8; j++)
            //    {
            //        var newDef = new ContentBlockDefinition
            //        {
            //            CategoryIds = def.CategoryIds.ToList(),
            //            DataTypeId = def.DataTypeId,
            //            DataTypeKey = def.DataTypeKey,
            //            Description = def.Description,
            //            Id = new Guid($"{i}{j}{def.Id.ToString().Substring(2)}"),
            //            Layouts = def.Layouts.Select(l => new ContentBlockLayout
            //            {
            //                Description = l.Description,
            //                Id = new Guid($"{i}{j}{l.Id.ToString().Substring(2)}"),
            //                Name = l.Name,
            //                PreviewImage = l.PreviewImage,
            //                ViewPath = l.ViewPath,
            //            }).ToList(),
            //            Name = def.Name + $" - copy #{j + 1}",
            //            PreviewImage = def.PreviewImage,
            //        };

            //        _definitions.Add(newDef);
            //    }
            //}

            //_presetRepository.Add(new ContentBlocksPreset
            //{
            //    Id = new Guid("5c03b99b-cce3-4c41-8de5-0a3d1055aee1"),
            //    Name = "Header",
            //    Blocks = new[]
            //    {
            //        new ContentBlockPreset
            //        {
            //            Id = new Guid("58f7a9ce-b4b7-4b43-a9b0-b25abd131bc8"),
            //            DefinitionId = new Guid("1c6e3935-fc13-426a-b522-87f4946567de"),
            //            LayoutId = new Guid("5722646d-2188-4a78-b74b-47446399d096"),
            //            Values =
            //            {
            //                ["title"] = "Preset Title value",
            //            },
            //        },
            //    }
            //});
        }

        private void CreateExampleBlock(string contentTypeAlias, Guid dataTypeKey)
        {
            CreateExampleBlockElementType(contentTypeAlias);
            CreateExampleBlockDataType(contentTypeAlias, dataTypeKey);
        }

        private void CreateExampleBlockElementType(string contentTypeAlias)
        {
            if (_contentTypeService.Get(contentTypeAlias) != null)
            {
                // Already created
                return;
            }

            IContentType contentType = new ContentType(_shortStringHelper, -1)
            {
                Alias = contentTypeAlias,
                IsElement = true,
                Name = "Header Block",
                PropertyGroups = new PropertyGroupCollection(new[]
                {
                    new PropertyGroup(new PropertyTypeCollection(true, new[]
                    {
                        new PropertyType(_shortStringHelper, PropertyEditors.Aliases.MediaPicker3, ValueStorageType.Ntext)
                        {
                            PropertyEditorAlias = PropertyEditors.Aliases.MediaPicker3,
                            Name = "Header Image",
                            Alias = "headerImage",
                        },
                        new PropertyType(_shortStringHelper, PropertyEditors.Aliases.TextBox, ValueStorageType.Ntext)
                        {
                            PropertyEditorAlias = PropertyEditors.Aliases.TextBox,
                            Name = "Title",
                            Alias = "title",
                        },
                        new PropertyType(_shortStringHelper, PropertyEditors.Aliases.TextBox, ValueStorageType.Ntext)
                        {
                            PropertyEditorAlias = PropertyEditors.Aliases.TinyMce,
                            Name = "Text",
                            Alias = "text",
                        },
                    }))
                    {
                        Alias = "content",
                        Name = "Content",
                    }
                })
            };

            _contentTypeService.Save(contentType);
        }

        private void CreateExampleBlockDataType(string contentTypeAlias, Guid dataTypeKey)
        {
            if (_dataTypeService.GetDataType(dataTypeKey) != null)
            {
                // Already there
                return;
            }

            if (!(_propertyEditors.TryGet("Umbraco.NestedContent", out var editor) && editor is NestedContentPropertyEditor nestedContentEditor))
            {
                throw new InvalidOperationException("Nested Content property editor not found!");
            }

            var dataType = new DataType(nestedContentEditor, _configurationEditorJsonSerializer, -1)
            {
                Name = "Perplex.ContentBlocks - HeaderBlock",
                Key = dataTypeKey,
                Configuration = new NestedContentConfiguration
                {
                    ConfirmDeletes = false,
                    HideLabel = true,
                    MinItems = 1,
                    MaxItems = 1,
                    ShowIcons = false,
                    ContentTypes = new[]
                    {
                        new NestedContentConfiguration.ContentType
                        {
                            Alias = contentTypeAlias,
                            TabAlias = "Content",
                            Template = "{{title}}"
                        }
                    }
                }
            };

            _dataTypeService.Save(dataType);
        }

        public void Terminate()
        {
        }
    }
}