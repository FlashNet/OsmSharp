using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using OsmSharp.UI.Renderer;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using OsmSharp.UI;
using OsmSharp.UI.Map.Styles.MapCSS;
using OsmSharp.UI.Map;
using OsmSharp.UI.Map.Layers;
using OsmSharp.Osm.Data.PBF.Processor;
using OsmSharp.Math.Geo;
using OsmSharp.Osm.Data.Xml.Processor;
using OsmSharp.Routing;
using OsmSharp.Routing.Osm.Interpreter;
using OsmSharp.Routing.Route;
using OsmSharp.Routing.Osm.Graphs.Serialization;
using OsmSharp.Routing.CH;
using OsmSharp.Routing.CH.Serialization;
using OsmSharp.Osm.Data.Memory;
using OsmSharp.UI.Renderer.Scene;

namespace OsmSharp.Android.UI.Sample
{
	/// <summary>
	/// Activity1.
	/// </summary>
	[Activity]
    public class MainActivity : Activity
	{
		/// <summary>
		/// Holds the router.
		/// </summary>
		private Router _router;

		/// <summary>
		/// Holds the route layer.
		/// </summary>
		private LayerOsmSharpRoute _routeLayer;

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

//			OsmSharp.IO.Output.OutputStreamHost.RegisterOutputStream (
//				new OsmSharp.Android.UI.IO.Output.ConsoleOutputStream ());
			
			// create the MapCSS image source.
			var imageSource = new MapCSSDictionaryImageSource();
			imageSource.Add("styles/default/parking.png",
			                Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.images.parking.png"));
			imageSource.Add("styles/default/bus.png",
			                Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.images.bus.png"));
			imageSource.Add("styles/default/postbox.png",
			                Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.images.postbox.png"));

//			// load mapcss style interpreter.
//			var mapCSSInterpreter = new MapCSSInterpreter(
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.test.mapcss"),
//				imageSource);
			
			// initialize the data source.
			//var dataSource = new MemoryDataSource();
//			var source = new XmlOsmStreamReader(
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.test.osm"));
//			var source = new PBFOsmStreamReader(
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.test.osm.pbf"));
//			dataSource.PullFromSource(source);

			// initialize map.
			var map = new Map();
			//map.AddLayer(new LayerTile(@"http://otile1.mqcdn.com/tiles/1.0.0/osm/{0}/{1}/{2}.png"));
			//map.AddLayer(new OsmLayer(dataSource, mapCSSInterpreter));
//			map.AddLayer(new LayerScene(Scene2DSimple.Deserialize(
//							Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.wvl.osm.pbf.scene.simple"), true)));
			map.AddLayer(
				new LayerScene(
				Scene2DLayered.Deserialize(
					Assembly.GetExecutingAssembly().GetManifestResourceStream(@"OsmSharp.Android.UI.Sample.wvl.map"), true)));

//			var routingSerializer = new V2RoutingDataSourceLiveEdgeSerializer(true);
//			var graphSerialized = routingSerializer.Deserialize(
//				//Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.test.osm.pbf.routing.3"));
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.wvl.pbf.routing.4"));
////
////			var graphLayer = new LayerDynamicGraphLiveEdge(graphSerialized, mapCSSInterpreter);
////			map.AddLayer(graphLayer);
//			
//			// calculate route.            
//			Router router = Router.CreateLiveFrom(
//				graphSerialized,
//				new OsmRoutingInterpreter());
			
//			var routingSerializer = new OsmSharp.Routing.CH.Serialization.Sorted.CHEdgeDataDataSourceSerializer(false);
//			var graphDeserialized = routingSerializer.Deserialize(
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.wvl.osm.pbf.routing.ch"), true);
////
//			_router = Router.CreateCHFrom(
//				graphDeserialized, new CHRouter(graphDeserialized),
//				new OsmRoutingInterpreter());

//
//			//GeoCoordinate point1 = new GeoCoordinate(51.158075, 2.961545);
//			//GeoCoordinate point2 = new GeoCoordinate(51.190503, 3.004793);
//			//GeoCoordinate point3 = new GeoCoordinate(51.175967, 2.93733);
//			GeoCoordinate point1 = new GeoCoordinate (50.885726, 3.253426);
//			//GeoCoordinate point2 = new GeoCoordinate (50.88602, 3.218149);
//			GeoCoordinate point2 = new GeoCoordinate (51.1515, 2.9563);
//			GeoCoordinate point3 = new GeoCoordinate(51.34643, 3.28837);
//
//			OsmSharpRoute route1 = router.Calculate(Vehicle.Car, 
//			                                       router.Resolve(Vehicle.Car, point1),
//			                                       router.Resolve(Vehicle.Car, point2));
//			
//			OsmSharpRoute route2 = router.Calculate(Vehicle.Car, 
//			                                       router.Resolve(Vehicle.Car, point1),
//			                                       router.Resolve(Vehicle.Car, point3));
//
//			OsmSharpRoute route3 = router.Calculate(Vehicle.Car, 
//			                                        router.Resolve(Vehicle.Car, point2),
//			                                        router.Resolve(Vehicle.Car, point3));
////
////			OsmSharpRoute route = router.Calculate(Vehicle.Car, 
////			                                       router.Resolve(Vehicle.Car, new GeoCoordinate(51.15136, 3.19462)),
////			                                       router.Resolve(Vehicle.Car, new GeoCoordinate(51.075023, 3.096632)));
//////			route = router.Calculate(Vehicle.Car, 
//////			                         router.Resolve(Vehicle.Car, new GeoCoordinate(51.075023, 3.096632)),
//////			                         router.Resolve(Vehicle.Car, new GeoCoordinate(51.15136, 3.19462)));
////			route = router.Calculate(Vehicle.Car, 
////			                         router.Resolve(Vehicle.Car, new GeoCoordinate(51.15136, 3.19462)),
////			                         router.Resolve(Vehicle.Car, new GeoCoordinate(51.075023, 3.096632)));
			_routeLayer = new LayerOsmSharpRoute(map.Projection);
//			osmSharpLayer.AddRoute (route1, SimpleColor.FromKnownColor(KnownColor.Blue).Value);
//			osmSharpLayer.AddRoute (route2, SimpleColor.FromKnownColor(KnownColor.Red).Value);
//			osmSharpLayer.AddRoute (route3, SimpleColor.FromKnownColor(KnownColor.YellowGreen).Value);
			map.AddLayer(_routeLayer);

//			// create gpx layer.
//			LayerGpx gpxLayer = new LayerGpx(map.Projection);
//			gpxLayer.AddGpx(
//				Assembly.GetExecutingAssembly().GetManifestResourceStream("OsmSharp.Android.UI.Sample.test.gpx"));
//			map.AddLayer(gpxLayer);
			
//			// set control properties.
//			var mapView = new MapView(this);
//			mapView.MapMaxZoomLevel = 20;
//			mapView.MapMinZoomLevel = 12;
//			//var mapView = new MapGLView (this);
//			mapView.Map = map;
//			//mapView.Center = new GeoCoordinate(51.158075, 2.961545); // gistel
//			//mapView.MapCenter = new GeoCoordinate (50.88672, 3.23899);
//			mapView.MapCenter = new GeoCoordinate(51.26337, 4.78739);
//			//mapView.Center = new GeoCoordinate(51.156803, 2.958887);
//			mapView.MapZoomLevel = 15;

//			var mapView = new OpenGLRenderer2D(
//				this, null);

			//var mapGLView = new MapGLView(this);

			var mapLayout = new MapView (this);
			mapLayout.Map = map;
			
			mapLayout.MapMaxZoomLevel = 20;
			mapLayout.MapMinZoomLevel = 12;
			mapLayout.MapTilt = 90;
			//var mapView = new MapGLView (this);
			mapLayout.MapCenter = new GeoCoordinate(51.158075, 2.961545); // gistel
			//mapView.MapCenter = new GeoCoordinate (50.88672, 3.23899);
			//mapLayout.MapCenter = new GeoCoordinate(51.26337, 4.78739);
			//mapView.Center = new GeoCoordinate(51.156803, 2.958887);
			mapLayout.MapZoomLevel = 15;
			mapLayout.MapTapEvent+= delegate(GeoCoordinate geoCoordinate) {
				mapLayout.AddMarker(geoCoordinate).Click  += new EventHandler (MapMarkerClicked);
			};

			//Create the user interface in code
			var layout = new RelativeLayout (this);
			//layout.Orientation = Orientation.Vertical;

			//layout.AddView(mapGLView);
			layout.AddView (mapLayout);

//			mapLayout.AddMarker (new GeoCoordinate (51.26337, 4.78739)).Click += new EventHandler (MapMarkerClicked);
//			mapLayout.AddMarker (new GeoCoordinate (51.26785, 4.78025)).Click += new EventHandler (MapMarkerClicked);

			SetContentView (layout);
		}

		/// <summary>
		/// Holds the previous point.
		/// </summary>
		private RouterPoint _previousPoint;

		private void MapMarkerClicked(object sender, EventArgs e)
		{
			if (sender is MapMarker) {
				lock (_router) {
					MapMarker marker = sender as MapMarker;
					RouterPoint point = _router.Resolve (Vehicle.Car, marker.Location);
					if (point != null) {
						if (_previousPoint != null) {
							_routeLayer.Clear ();
							OsmSharpRoute route = _router.Calculate (Vehicle.Car, _previousPoint, point);
							if (route != null) {
								_routeLayer.AddRoute (route);
							}
							_routeLayer.Invalidate ();
						}
						_previousPoint = point;
					}
				}
			}
		}
	}
}