namespace LearningCenter.Services.Data.Tests
{
    using System.Reflection;

    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using Xunit;

    public class AboutServiceTests
    {
        public AboutServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
        }
    }
}
