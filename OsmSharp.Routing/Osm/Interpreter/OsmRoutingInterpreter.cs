﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Routing.Interpreter;
using OsmSharp.Routing.Constraints;
using OsmSharp.Routing.Interpreter.Roads;

namespace OsmSharp.Routing.Osm.Interpreter
{
    /// <summary>
    /// A routing interpreter for OSM data.
    /// </summary>
    public class OsmRoutingInterpreter : IRoutingInterpreter
    {
        /// <summary>
        /// Holds the edge interpreter.
        /// </summary>
        private readonly IEdgeInterpreter _edgeInterpreter;

        /// <summary>
        /// Holds the routing constraints.
        /// </summary>
        private readonly IRoutingConstraints _constraints;

        /// <summary>
        /// Holds the relevant keys.
        /// </summary>
        private HashSet<string> _relevantKeys; 

        /// <summary>
        /// Creates a new routing intepreter with default settings.
        /// </summary>
        public OsmRoutingInterpreter()
        {
            _edgeInterpreter = new Edge.EdgeInterpreter();
            _constraints = null;

            this.FillRelevantTags();
        }

        /// <summary>
        /// Creates a new routing interpreter with given constraints.
        /// </summary>
        /// <param name="constraints"></param>
        public OsmRoutingInterpreter(IRoutingConstraints constraints)
        {
            _edgeInterpreter = new Edge.EdgeInterpreter();
            _constraints = constraints;
            
            this.FillRelevantTags();
        } 
	        
        /// <summary>
        /// Creates a new routing interpreter a custom edge interpreter.
        /// </summary>
        /// <param name="interpreter"></param>
        public OsmRoutingInterpreter(IEdgeInterpreter interpreter)
        {
            _edgeInterpreter = interpreter;
            _constraints = null;
        }	  

        /// <summary>
        /// Builds the list of relevant tags.
        /// </summary>
        private void FillRelevantTags()
        {
            _relevantKeys = new HashSet<string> { "oneway", "highway", "name", "motor_vehicle", "bicycle", "foot", "access", "maxspeed", "junction" };
        }

        /// <summary>
        /// Returns true if the given tags is relevant.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsRelevant(string key)
        {
            return _relevantKeys.Contains(key);
        }

        /// <summary>
        /// Returns true if the given vertices can be traversed in the given order.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="along"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public bool CanBeTraversed(long from, long along, long to)
        {
            return true;
        }

        /// <summary>
        /// Returns and edge interpreter.
        /// </summary>
        public IEdgeInterpreter EdgeInterpreter
        {
            get 
            {
                return _edgeInterpreter; 
            }
        }

        /// <summary>
        /// Returns the constraints.
        /// </summary>
        public IRoutingConstraints Constraints
        {
            get
            {
                return _constraints;
            }
        }
    }
}