using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsmSharp.Osm.Data.Xml.Processor;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using OsmSharp.Data.SQLServer.Osm.SchemaTools;
using OsmSharp.Data.SQLServer.Osm.Streams;

namespace UnitTestProject2
{
  [TestClass]
  public class UnitTest1
  {

    [TestMethod]
    public void TestMethod1()
    {
      var source = new XmlOsmStreamSource(@"C:\ms\RU-KGD.osm");

      const string connectionString = @"Server=localhost;Database=fcsdbo;User Id=sa;Password=sa123;";

      var dbConnection = new SqlConnection(connectionString);
      dbConnection.Open();
      SQLServerSchemaTools.Remove(dbConnection);
      SQLServerSchemaTools.CreateAndDetect(dbConnection);
      SQLServerSchemaTools.AddConstraints(dbConnection);

      var target = new SQLServerOsmStreamTarget(dbConnection);
      target.RegisterSource(source);
      target.Pull();
    }
  }
}
