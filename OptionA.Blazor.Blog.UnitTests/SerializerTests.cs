using System.Text.Json;

namespace OptionA.Blazor.Blog.UnitTests
{
    public class SerializerTests
    {
        [Fact]
        public void SerializazitonYieldsSameResults()
        {
            var post = GetPost();
            var json = post.Serialize(new JsonSerializerOptions { WriteIndented = true });

            var repost = Post.Deserialize(json);
            var rejson = repost.Serialize(new JsonSerializerOptions { WriteIndented = true });

            Assert.Equal(json, rejson);            
        }

        [Fact]
        public void SerializeWithOnClickThrowsException()
        {
            var test = new BlockContent();
            test.OnClick = (args) =>
            {
                return Task.CompletedTask;
            };

            Assert.Throws<NotSupportedException>(test.GetSerializationData);
        }

        [Fact]
        public void SerializeWithCustomThrowsException()
        {
            var builder = Post.CreateEmptyBuilder();
            builder
                .AddCustom(typeof(Block));
            var post = builder.Build();
            Assert.Throws<NotSupportedException>(() => post.Serialize());
        }

        private Post GetPost()
        {
            var builder = Post.CreateEmptyBuilder();
            builder
                .WithDate(2022, 11, 3)
                .WithTitle("My first Blog post")
                .WithSubtitle("This post is about setting everyting up correctly")
                .WithTags("blazor", "setup")
                .AddParagraph("If i place tags <div>test me</div> here what happens?")
                .CreateHeader()
                    .OfSize(HeaderSize.One)
                    .CreateBlock()
                        .AddIcon("bi bi-check-circle")
                        .AddContent("Option A")
                        .Build()
                    .CreateBlock()
                        .AddIcon("bi bi-circle")
                        .AddContent("Option B")
                        .Build()
                    .Build()
                .AddHeader("Test header", HeaderSize.Three)
                .CreateParagraph()
                    .WithStyle(Style.StrikeThrough | Style.UpperCase)
                    .WithText("Lorum ipsum something more")
                    .Build()
                .CreateParagraph()
                    .AddContent("Some text")
                    .AddSpace()
                    .AddLink("link", "https://www.nu.nl")
                    .Build()
                .AddQuote("This is MY blog", "Erik", "https://option-a.tech")
                .AddCode(CodeLanguage.CSharp, """
                    {
                        SomeFunction.CallMe();
                        var x = 12;
                        Label y = *M*typeof(Test)*M*;
                        Labeler z = nameof(Label);
                        return x;
                        var testMeAsweel = *M*$"{a*M*a} *M*asn*M* more text";
                        var testMeMore*M* = "yo yo sem";*M*
                        var hard = @"multi
                        line
                        string";
                    }
                    """)
                .AddCode(CodeLanguage.Html, """
                    @using Blog.Navigation
                    @inherits LayoutComponentBase

                    <div class="page">
                        <NavMenu />
                        <main> <!-- Some comments here -->
                            <article class="content px-4">
                                <div class="row">
                                    <div class="col-lg-8">
                                        @Body
                       <!--   dont want this part          </div>
                                    <div class="col-lg-4">
                                        <TagContainer />
                                        <ArchiveContainer />                    
                                   --> </div> 
                                </div>            
                            </article>
                        </main>
                    </div>
                    """)
                .CreateTable()
                    .WithColumns("First", "Second", string.Empty)
                    .AddStyle(Style.StrikeThrough)
                    .AddRow("test", 23, true)
                    .RemoveStyle(Style.StrikeThrough)
                    .AddRow("Mijn naam is haas", "pie", false)
                    .AddRow("temp", null, true)
                    .Build()
                .AddImage("OptionALogoFull.png", "Something only we know")
                .AddParagraph("This was all made using just this code:")
                .AddImage("postcontent.png")
                .AddFooter("Might need more work")
                .RemoveStyle(Style.Bold)
                .CreateList()
                    .WithBlockType(BlockType.Content)
                    .AddRow("item 1")
                    .AddRow(4)
                    .AddRow("applePie")
                    .Build()
                .CreateList()
                    .IsOrdered(true)
                    .WithBlockType(BlockType.Content)
                    .WithListStyle(ListStyle.UpperRoman)
                    .AddRow("item 1")
                    .AddRow(4)
                    .AddRow("cherry pie")
                    .Build()
                .CreateOrderedList(4)
                    .AddRow("item 1")
                    .AddRow(4)
                    .AddRow("on top?")
                    .Build();

            return builder.Build();
        }
    }
}
