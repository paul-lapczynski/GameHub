using LanPartyHub.Models;
using LanPartyHub.Models.DOSBox;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace GameHubTests
{
    public class UnitTest1
    {
        [Fact]
        public void Default_Setting_Is_Overridden()
        {
            var gg = new DOSBoxManagerTest();
            var settings = new DOSBoxConfigSettings
            {
                Sections = new List<DOSBoxConfigSection> {
                    new DOSBoxConfigSection
                    {
                        Name = "[sdl]",
                        Settings = new List<DOSBoxSetting>
                    {
                        new DOSBoxSetting
                        {
                            Name ="Setting_1",
                            DefaultValue = "One",
                            Description = "One 1",
                            Section = "[sdl]",
                            Values = new List<string>()
                        }
                    }
                    }
                }
            };

            var overrideSettings = new List<DOSBoxSettingOverride>{ new DOSBoxSettingOverride
            {
                Name = "Setting_1",
                Section = "[sdl]",
                SelectedValue = "2" }
            };
            var configString = gg.BuildDOSBoxConfigFile(settings, overrideSettings);

            Assert.Equal($"[sdl]{Environment.NewLine}Setting_1=2{Environment.NewLine}", configString);
        }

        [Fact]
        public void Empty_DOSBoxSettingOverride_List_Is_Passed_And_Should_Use_Default_Value()
        {
            var gg = new DOSBoxManagerTest();
            var settings = new DOSBoxConfigSettings
            {
                Sections = new List<DOSBoxConfigSection> {
                    new DOSBoxConfigSection
                    {
                        Name = "[sdl]",
                        Settings = new List<DOSBoxSetting>
                    {
                        new DOSBoxSetting
                        {
                            Name ="Setting_1",
                            DefaultValue = "One",
                            Description = "One 1",
                            Section = "[sdl]",
                            Values = new List<string>()
                        }
                    }
                    }
                }
            };

            var overrideSettings = new List<DOSBoxSettingOverride>();

            var configString = gg.BuildDOSBoxConfigFile(settings, overrideSettings);

            Assert.Equal($"[sdl]{Environment.NewLine}Setting_1=One{Environment.NewLine}", configString);
        }

        [Fact]
        public void Save_DOSBox_File()
        {
            var gg = new DOSBoxManagerTest();
            var overrideSettings = new List<DOSBoxSettingOverride>();
        
            var createFile = gg.CreatedConfigFileForGame(overrideSettings);

            Assert.True(createFile);
        }
    }
}
