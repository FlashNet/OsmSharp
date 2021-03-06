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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace OsmSharp.UnitTests
{
    /// <summary>
    /// Contains tests for the (extension) methods in the utilities class.
    /// </summary>
    [TestFixture]
    public class UtilitiesTest
    {
        /// <summary>
        /// Tests the unixtime conversion.
        /// </summary>
        [Test]
        public void TestUnixTime()
        {
            DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

            long unixTime = time.ToUnixTime();
            DateTime timeAfter = unixTime.FromUnixTime();

            Assert.AreEqual(time, timeAfter);

            unixTime = 1374842318000;
            time = unixTime.FromUnixTime();
            long unixTimeAfter = time.ToUnixTime();

            Assert.AreEqual(unixTime, unixTimeAfter);
        }

        /// <summary>
        /// Tests the shuffling of a list.
        /// </summary>
        [Test]
        public void TestShuffle()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            Utilities.Shuffle(list);

            Assert.AreEqual(5, list.Count);
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsTrue(list.Contains(3));
            Assert.IsTrue(list.Contains(4));
            Assert.IsTrue(list.Contains(5));
        }

        /// <summary>
        /// Tests the initcap function.
        /// </summary>
        [Test]
        public void TestInitCap()
        {
            string uncapped = "open street map";
            string capped = "Open Street Map";

            Assert.AreEqual(capped, uncapped.InitCap());
        }

        /// <summary>
        /// Tests the levenstein maching algorithm.
        /// </summary>
        [Test]
        public void TestLevensteinMatch()
        {
            string reference = "openstreetmap";

            string referenceok = "openstreetmaps";
            string referencenok = "googlemaps";

            Assert.IsTrue(reference.LevenshteinMatch(referenceok, 90));
            Assert.IsFalse(reference.LevenshteinMatch(referencenok, 90));
        }

        /// <summary>
        /// Tests the numeric part float parser.
        /// </summary>
        [Test]
        public void TestNumericPartFloat()
        {
            Assert.AreEqual("10.0", "10.0".NumericPartFloat());
            Assert.AreEqual("", "ten".NumericPartFloat());
            Assert.AreEqual("10", "10A".NumericPartFloat());
            Assert.AreEqual("10", "10.B".NumericPartFloat());
        }

        /// <summary>
        /// Tests the numeric part int parser.
        /// </summary>
        [Test]
        public void TestNumericPartInt()
        {
            Assert.AreEqual("10", "10.0".NumericPartInt());
            Assert.AreEqual("", "ten".NumericPartInt());
            Assert.AreEqual("10", "10A".NumericPartInt());
            Assert.AreEqual("10", "10.B".NumericPartInt());
        }

        /// <summary>
        /// Tests the split multiple function.
        /// </summary>
        [Test]
        public void TestSplitMultiple()
        {
            string test = "openstreetmap";
            int[] sizes = new int[] { 4, 6, 3 };

            string[] split = test.SplitMultiple(sizes);
            Assert.AreEqual(3, split.Length);
            Assert.AreEqual("open", split[0]);
            Assert.AreEqual("street", split[1]);
            Assert.AreEqual("map", split[2]);
        }

        /// <summary>
        /// Tests to string empty when null.
        /// </summary>
        [Test]
        public void TestToStringEmptyWhenNull()
        {
            string test = "openstreetmap";
            Assert.AreEqual(test, test.ToStringEmptyWhenNull());
            test = null;
            Assert.AreEqual(string.Empty, test.ToStringEmptyWhenNull());
        }

        /// <summary>
        /// Tests the trunctate function.
        /// </summary>
        [Test]
        public void TestTruncate()
        {
            string test = "openstreetmap";

            Assert.AreEqual(test, test.Truncate(10000));
            Assert.AreEqual("open", test.Truncate(4));
        }

        /// <summary>
        /// Tests the pad right function.
        /// </summary>
        [Test]
        public void TestPadRight()
        {
            string test = "open";

            Assert.AreEqual(test, test.PadRightAndCut(4));
            Assert.AreEqual("open ", test.PadRightAndCut(5));
            Assert.AreEqual("ope", test.PadRightAndCut(3));
        }
    }
}
