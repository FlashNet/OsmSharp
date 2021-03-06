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
using OsmSharp.Data.PostgreSQL.Osm;
using OsmSharp.Osm.Data.Streams;
using OsmSharp.Data.PostgreSQL.Osm.Streams;
using NUnit.Framework;
using Npgsql;

namespace OsmSharp.Data.Unittests.PostgreSQL
{
    /// <summary>
    /// Contains database tests for PostgreSQL and osm-data.
    /// </summary>
    [TestFixture]
    public class PostgreSQLProviderOsmTests : DataProviderOsmTests
    {
        /// <summary>
        /// Tests read/write node.
        /// </summary>
        [Test]
        public void PostgreSQLTestNodeReadWrite()
        {
            base.TestNodeReadWrite();
        }

        /// <summary>
        /// Tests read/write way.
        /// </summary>
        [Test]
        public void PostgreSQLTestWayReadWrite()
        {
            base.TestWayReadWrite();
        }

        /// <summary>
        /// Tests read/write relation.
        /// </summary>
        [Test]
        public void PostgreSQLTestRelationReadWrite()
        {
            base.TestRelationReadWrite();
        }

        /// <summary>
        /// Tests a write to a PostgreSQL database and verifies all the data.
        /// </summary>
        [Test]
        public void PostgreSQLTestReadWriteData()
        {
            base.TestReadWriteData();
        }

        /// <summary>
        /// Tests a write to a PostgreSQL database and gets ways using it's nodes.
        /// </summary>
        [Test]
        public void PostgreSQLTestGetWaysForNode()
        {
            base.TestGetWaysForNode();
        }

        /// <summary>
        /// Tests a write to a PostgreSQL database and gets relations using it's members.
        /// </summary>
        [Test]
        public void PostgreSQLTestGetRelationsForMember()
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

        #region PostgreSQL Implementations

        private NpgsqlConnection _connection = null;

        public override void NotifyEmptyExpected()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
            _connection = null;
        }

        private NpgsqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new NpgsqlConnection("Server=TestDataLinux;Port=5432;Database=osm;User Id=osmsharp;Password=osmsharp;");
                _connection.Open();

                PostgreSQLSchemaTools.Drop(_connection);
            }
            return _connection;
        }

        public override IDataSourceReadOnly CreateDataSource()
        {
            return new PostgreSQLDataSource(this.GetConnection());
        }

        public override OsmStreamTarget CreateDataStreamTarget()
        {
            return new PostgreSQLOsmStreamTarget(this.GetConnection());
        }

        #endregion
    }
}
