﻿// OsmSharp - OpenStreetMap (OSM) SDK
// Copyright (C) 2013 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.

using System.IO;
using System.Reflection;
using Antlr.Runtime;
using OsmSharp.UI.Map.Styles.MapCSS.v0_2;
using OsmSharp.UI.Map.Styles.MapCSS.v0_2.Domain;
using OsmSharp.Collections.Tags;
using OsmSharp.UI.Map.Styles.MapCSS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OsmSharp.UI.Unittests.Map.Styles.MapCSS.v0_2
{
    /// <summary>
    /// Contains parsing tests using existing mapcss files.
    /// </summary>
    [TestClass]
    public class MapCSSDomainParserTests
    {
        /// <summary>
        /// Tests simply parsing one of the testcases.
        /// </summary>
        [TestMethod]
        public void TestDomainDefault()
        {
            // parses the MapCSS.
            AstParserRuleReturnScope<object, IToken> result = this.TestMapCSSParsing(
                "UnitTestProject2.Data.MapCSS.default.mapcss");

            // Test the very minimum; no errors during parsing says a lot already!
            var tree = result.Tree as Antlr.Runtime.Tree.CommonTree;
            Assert.IsNotNull(tree);
            Assert.AreEqual(54, tree.ChildCount);

            // parse into domain.
            MapCSSFile file = MapCSSDomainParser.Parse(tree);
            Assert.IsNotNull(file);
        }

        /// <summary>
        /// Regression test parsing a named color.
        /// </summary>
        [TestMethod]
        public void TestDomainColorNamed()
        {
            // parses the MapCSS.
            AstParserRuleReturnScope<object, IToken> result = this.TestMapCSSParsing(
                "UnitTestProject2.Data.MapCSS.color-named.mapcss");

            // Test the very minimum; no errors during parsing says a lot already!
            var tree = result.Tree as Antlr.Runtime.Tree.CommonTree;
            Assert.IsNotNull(tree);
            Assert.AreEqual(2, tree.ChildCount);

            // parse into domain.
            MapCSSFile file = MapCSSDomainParser.Parse(tree);
            Assert.IsNotNull(file);
            Assert.AreEqual(1, file.Rules.Count);
            Assert.AreEqual(1, file.Rules[0].Declarations.Count);
            Assert.IsInstanceOfType(file.Rules[0].Declarations[0], typeof(DeclarationInt));

            // get color declaration.
            var declarationInt = file.Rules[0].Declarations[0] as DeclarationInt;
            Assert.IsNotNull(declarationInt);
            Assert.AreEqual(DeclarationIntEnum.Color, declarationInt.Qualifier);

            // instantiate color.
            var simpleColor = new SimpleColor();
            simpleColor.Value = declarationInt.Eval((MapCSSObject)null);
            Assert.AreEqual("#FFFFFF", simpleColor.HexRgb);
        }

        /// <summary>
        /// Tests a meta settings CSS.
        /// 
        /// Meta settings example:
        /// meta {
        ///     title: "Parking lanes";   /* title shown in the menu */
        ///     icon: "images/logo.png";  /* small icon shown in the menu next to the title */
        /// }
        /// </summary>
        [TestMethod]
        public void TestMetaSettingsCSS()
        {
            // create CSS.
            string css = "meta { " +
                "   title: \"Parking lanes\"; /* title shown in the menu */ " +
                "   icon: \"images/logo.png\"; /* small icon shown in the menu next to the title */ " + 
                "} ";

            // parses the MapCSS.
            AstParserRuleReturnScope<object, IToken> result = this.TestMapCSSParsingString(css);

            // Test the very minimum; no errors during parsing says a lot already!
            var tree = result.Tree as Antlr.Runtime.Tree.CommonTree;
            Assert.IsNotNull(tree);
            Assert.AreEqual(1, tree.ChildCount);

            // parse into domain.
            MapCSSFile file = MapCSSDomainParser.Parse(tree);
            Assert.IsNotNull(file);
            Assert.AreEqual(0, file.Rules.Count);

            Assert.AreEqual("Parking lanes", file.Title);
            Assert.AreEqual("images/logo.png", file.Icon);
        }

        /// <summary>
        /// Regression test parsing a short color.
        /// </summary>
        [TestMethod]
        public void TestDomainColorShort()
        {
            // parses the MapCSS.
            AstParserRuleReturnScope<object, IToken> result = this.TestMapCSSParsing(
                "UnitTestProject2.Data.MapCSS.color-short.mapcss");

            // Test the very minimum; no errors during parsing says a lot already!
            var tree = result.Tree as Antlr.Runtime.Tree.CommonTree;
            Assert.IsNotNull(tree);
            Assert.AreEqual(2, tree.ChildCount);

            // parse into domain.
            MapCSSFile file = MapCSSDomainParser.Parse(tree);
            Assert.IsNotNull(file);
            Assert.AreEqual(1, file.Rules.Count);
            Assert.AreEqual(1, file.Rules[0].Declarations.Count);
            Assert.IsInstanceOfType(file.Rules[0].Declarations[0], typeof(DeclarationInt));

            // get color declaration.
            var declarationInt = file.Rules[0].Declarations[0] as DeclarationInt;
            Assert.IsNotNull(declarationInt);
            Assert.AreEqual(DeclarationIntEnum.Color, declarationInt.Qualifier);

            // instantiate color.
            var simpleColor = new SimpleColor();
            simpleColor.Value = declarationInt.Eval((MapCSSObject)null);
            Assert.AreEqual("#665555", simpleColor.HexRgb);
        }

        /// <summary>
        /// Regression test parsing a color.
        /// </summary>
        [TestMethod]
        public void TestDomainColor()
        {
            // parses the MapCSS.
            AstParserRuleReturnScope<object, IToken> result = this.TestMapCSSParsing(
                "UnitTestProject2.Data.MapCSS.color.mapcss");

            // Test the very minimum; no errors during parsing says a lot already!
            var tree = result.Tree as Antlr.Runtime.Tree.CommonTree;
            Assert.IsNotNull(tree);
            Assert.AreEqual(2, tree.ChildCount);

            // parse into domain.
            MapCSSFile file = MapCSSDomainParser.Parse(tree);
            Assert.IsNotNull(file);
            Assert.AreEqual(1, file.Rules.Count);
            Assert.AreEqual(1, file.Rules[0].Declarations.Count);
            Assert.IsInstanceOfType(file.Rules[0].Declarations[0], typeof(DeclarationInt));

            // get color declaration.
            var declarationInt = file.Rules[0].Declarations[0] as DeclarationInt;
            Assert.IsNotNull(declarationInt);
            Assert.AreEqual(DeclarationIntEnum.Color, declarationInt.Qualifier);

            // instantiate color.
            var simpleColor = new SimpleColor();
            simpleColor.Value = declarationInt.Eval((MapCSSObject)null);
            Assert.AreEqual("#00FF00", simpleColor.HexRgb);
        }

        /// <summary>
        /// Test-parses the MapCSS.
        /// </summary>
        /// <param name="embeddedPath"></param>
        private AstParserRuleReturnScope<object, IToken> TestMapCSSParsing(string embeddedPath)
        {
            // get the text from the embedded test file.
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedPath);
            Assert.IsNotNull(stream);
            var reader = new StreamReader(stream);
            string s = reader.ReadToEnd();

            return this.TestMapCSSParsingString(s);
        }

        /// <summary>
        /// Test-parses the MapCSS.
        /// </summary>
        /// <param name="css"></param>
        /// <returns></returns>
        private AstParserRuleReturnScope<object, IToken> TestMapCSSParsingString(string css)
        {
            var input = new ANTLRStringStream(css);
            var lexer = new MapCSSLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MapCSSParser(tokens);

            return parser.stylesheet();
        }
    }
}
