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
using OsmSharp.Osm.Data;
using OsmSharp.Data.SQLServer.Osm;
using OsmSharp.Osm.Data.Streams;
using OsmSharp.Data.SQLServer.Osm.Streams;
using NUnit.Framework;
using System.Data.SqlClient;
using OsmSharp.Data.SQLServer.Osm.SchemaTools;

namespace OsmSharp.Data.Unittests.SQLServer
{
    /// <summary>
    /// Contains database tests for SQLServer and osm-data.
    /// </summary>
    [TestFixture]
    public class SQLServerProviderOsmTests : DataProviderOsmTests
    {
        /// <summary>
        /// Tests read/write node.
        /// </summary>
        [Test]
        public void SQLServerTestNodeReadWrite()
        {
            base.TestNodeReadWrite();
        }

        /// <summary>
        /// Tests read/write way.
        /// </summary>
        [Test]
        public void SQLServerTestWayReadWrite()
        {
            base.TestWayReadWrite();
        }

        /// <summary>
        /// Tests read/write relation.
        /// </summary>
        [Test]
        public void SQLServerTestRelationReadWrite()
        {
            base.TestRelationReadWrite();
        }

        /// <summary>
        /// Tests a write to a SQLServer database and verifies all the data.
        /// </summary>
        [Test]
        public void SQLServerTestReadWriteData()
        {
            base.TestReadWriteData();
        }

        /// <summary>
        /// Tests a write to a SQLServer database and gets ways using it's nodes.
        /// </summary>
        [Test]
        public void SQLServerTestGetWaysForNode()
        {
            base.TestGetWaysForNode();
        }

        /// <summary>
        /// Tests a write to a SQLServer database and gets relations using it's members.
        /// </summary>
        [Test]
        public void SQLServerTestGetRelationsForMember()
        {
            base.TestGetRelationsForMember();
        }

        /// <summary>
        /// Tests a bounding box query.
        /// </summary>
        [Test]
        public void SQLiteTestBoundingBoxQueries()
        {
            base.TestBoundingBoxQueries();
        }

        #region SQLServer Implementations

        private SqlConnection _connection = null;

        public override void NotifyEmptyExpected()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
            _connection = null;
        }

        private SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(@"Server=TestDataWindows\SQLEXPRESS;Database=osmsharp;User Id=osmsharp;Password=osmsharp;");
                _connection.Open();

                SQLServerSchemaTools.Remove(_connection);
            }
            return _connection;
        }

        public override IDataSourceReadOnly CreateDataSource()
        {
            return new SQLServerDataSource(this.GetConnection());
        }

        public override OsmStreamTarget CreateDataStreamTarget()
        {
            return new SQLServerOsmStreamTarget(this.GetConnection(), true);
        }

        #endregion
    }
}