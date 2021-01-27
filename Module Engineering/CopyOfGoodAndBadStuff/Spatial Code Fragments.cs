        // Determines the shortest distance between a line and a point
        private double GetLineToPointDistance(MapLineLayer theLayer, GeoAPI.Geometries.Coordinate theCoordinate)
        {
            double minimumDistance = 9999999999999;
            for (int i = 0; i < theLayer.DataSet.Vertex.Length; i+= 2)
            {
                GeoAPI.Geometries.Coordinate linePoint = new GeoAPI.Geometries.Coordinate(theLayer.DataSet.Vertex[i], theLayer.DataSet.Vertex[i + 1]);
                minimumDistance = Math.Min(minimumDistance, GetCoordinateToCoordinateDistance(linePoint, theCoordinate));
            }
/*                        DotSpatial.Data.IFeatureSet theFeatureSet = theLayer.FeatureSet;
                        for (int lineNumber = 0; lineNumber < theFeatureSet.Features.Count; lineNumber++)
                        {
                            LineLayer theLine = theFeatureSet.Features[lineNumber].Geometry as LineLayer;
                            DotSpatial.Data.IFeatureSet lineFeatureSet = theLine.FeatureSet;
                            for (int i = 0; i < lineFeatureSet.Features.Count; i++)
                            {
                                DotSpatial.Topology.Point linePoint = lineFeatureSet.Features[i] as DotSpatial.Topology.Point;
                                minimumDistance = Math.Min(minimumDistance, GetPointToCoordinateDistance(linePoint, theCoordinate));
                            }
        }*/

            return minimumDistance;
        }

        // Determines the distance between two points assuming lat / long projection
        private double GetCoordinateToCoordinateDistance(GeoAPI.Geometries.Coordinate point1, GeoAPI.Geometries.Coordinate theCoordinate)
        {
            double latitudeDistance = (point1.Y - theCoordinate.Y) * Math.Cos(point1.Y * Math.PI / 180);
            double longitudeDistance = point1.X - theCoordinate.X;
            return Math.Sqrt(Math.Pow(latitudeDistance, 2) + Math.Pow(longitudeDistance, 2));
        }

        // Determines if two lines each defined by two points cross each other between the points
        private bool LinesIntersect(GeoAPI.Geometries.Coordinate line1Point1, GeoAPI.Geometries.Coordinate line1Point2, GeoAPI.Geometries.Coordinate line2Point1, GeoAPI.Geometries.Coordinate line2Point2)
        {
            // Line 1 is vertical
            if (line1Point1.X == line1Point2.X)
            {
                // Line 2 is also vertical
                if (line2Point1.X == line2Point2.X)
                {
                    // Not the same X value
                    if (line1Point1.X != line2Point1.X)
                        return false;

                    // Lines intersect if either end of the first line to see if it is within the points of the second line
                    return ((line1Point1.Y - line2Point1.Y) * (line1Point1.Y - line2Point2.Y) <= 0 ||
                            (line1Point2.Y - line2Point1.Y) * (line1Point2.Y - line2Point2.Y) <= 0);
                }
            }

            // Determine slopes
            double slope1 = (line1Point2.Y - line1Point1.Y) / (line1Point2.X - line1Point1.X);
            double slope2 = (line2Point2.Y - line2Point1.Y) / (line2Point2.X - line2Point1.X);

            // Determine intercepts
            double intercept1 = line1Point1.Y - slope1 * line1Point1.X;
            double intercept2 = line2Point1.Y - slope2 * line2Point1.X;

            // Parallel
            if (slope1 == slope2)
                return (intercept1 == intercept2);

            // Solve for x at interception
            double interceptionX = (intercept2 - intercept1) / (slope1 - slope2);

            // Lines intersect if intersection is within the endpoints of either (both) of the lines
            return ((interceptionX - line1Point1.X) * (interceptionX - line1Point2.X) < 0);
        }

        // Checks to see if a point is within a polygon using robust ray crossing method
        // Polygon vertices are in a single array of the form (x1, y1, x2, y2, x3, y3, ...)
        private bool CoordinateInPolygon(GeoAPI.Geometries.Coordinate theCoordinate, double [] polygonVertices)
        {
            // Polygon has no vertices(
            if (polygonVertices.Length < 2)
                return false;

            // Get bounding box of polygon
            double minX = polygonVertices[0];
            double maxX = polygonVertices[0];
            double minY = polygonVertices[1];
            double maxY = polygonVertices[1];
            for (int i = 2; i < polygonVertices.Length; i+= 2)
            {
                minX = Math.Min(minX, polygonVertices[i]);
                maxX = Math.Max(maxX, polygonVertices[i]);
                minY = Math.Min(minY, polygonVertices[i + 1]);
                maxY = Math.Max(maxY, polygonVertices[i + 1]);
            }

            // Get coordinate outside the bounding box
            GeoAPI.Geometries.Coordinate outsideCoordinate = new GeoAPI.Geometries.Coordinate(2 * maxX - minX, 2.001 * maxY - minY);

            // Check how many times a line between the coordinates crosses lines between polygon vertices
            int numCrosses = 0;
            for (int i = 0; i < polygonVertices.Length; i+= 2)
            {
                GeoAPI.Geometries.Coordinate polygonCoordinate1 = new GeoAPI.Geometries.Coordinate(polygonVertices[i], polygonVertices[i + 1]);
                
                // Second point is the previous point unless this is the first point, in which case the second point is the last vertex
                GeoAPI.Geometries.Coordinate polygonCoordinate2 = new GeoAPI.Geometries.Coordinate();
                if (i >= 2)
                {
                    polygonCoordinate2.X = polygonVertices[i - 2];
                    polygonCoordinate2.Y = polygonVertices[i - 1];
                }
                else
                {
                    polygonCoordinate2.X = polygonVertices[polygonVertices.Length - 2];
                    polygonCoordinate2.Y = polygonVertices[polygonVertices.Length - 1];
                }

                if (LinesIntersect(theCoordinate, outsideCoordinate, polygonCoordinate1, polygonCoordinate2) == true)
                    numCrosses++;
            }

            // Odd number of crosses means the point is in the polygon
            return (numCrosses % 2 == 1);
        }

        private bool GetShapeAndLayerFromCoordinates(GeoAPI.Geometries.Coordinate theCoordinates, ref int layerNumber, ref int featureNumber)
        {
            // Check layers from front to back to find the first which has been clicked upon
            for (layerNumber = mainMap.Layers.Count - 1; layerNumber >= 0; layerNumber--)
            {
                // Check if the layer is polygons
                MapPolygonLayer thePolygonLayer = mainMap.Layers[layerNumber] as MapPolygonLayer;
                if (thePolygonLayer != null && thePolygonLayer.DataSet.Vertex.Length > 0)
                {
                    if (CoordinateInPolygon(theCoordinates, thePolygonLayer.DataSet.Vertex) == true)
                        return true;
/*                    DotSpatial.Data.IFeatureSet theFeatureSet = thePolygonLayer.FeatureSet;
                    for (featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)(
                    {
                        Polygon thePolygon = theFeatureSet.Features[featureNumber].Geometry as Polygon;
                        if (thePolygon != null)
                        {
                            if (thePolygon.Contains(theCoordinates as IPoint))
                                return true;
                        }
                    }*/
                }
                else
                {
                    // If the layer isn't polygons (lines or points), figure out whether it is selected by
                    // creating a circular polygon around the double-click coordinate and finding the intersection with a layer

                    // Get the longitude distance for 2 pixels
                    System.Drawing.Point pixelCoordinates = mainMap.ProjToPixel(theCoordinates);
                    pixelCoordinates.Y += 2;
                    double latitudeRadius = Math.Abs(mainMap.PixelToProj(pixelCoordinates).Y - theCoordinates.Y);
                    // Create a circular polygon around the double-click point with the above radius
                    FeatureSet circleFeatureSet = new FeatureSet(DotSpatial.Data.FeatureType.Polygon);
                    int pointsInCircle = 50;
                    double[] circleVertices = new double[pointsInCircle * 2];
//                    Coordinate[] circleCoordinates = new Coordinate[pointsInCircle];
                    for (int i = 0; i < pointsInCircle; i++)
                    {
                        //                        circleCoordinates[i] = new Coordinate();
//                        circleCoordinates[i].X = theCoordinates.X + latitudeRadius * Math.Cos(2 * Math.PI * i / pointsInCircle);
//                        circleCoordinates[i].Y = theCoordinates.Y + latitudeRadius * Math.Sin(2 * Math.PI * i / pointsInCircle) * Math.Cos(theCoordinates.Y * Math.PI / 180);
                        circleVertices[i * 2] = theCoordinates.X + latitudeRadius * Math.Cos(2 * Math.PI * i / pointsInCircle);
                        circleVertices[i * 2 + 1] = theCoordinates.Y + latitudeRadius * Math.Sin(2 * Math.PI * i / pointsInCircle) * Math.Cos(theCoordinates.Y * Math.PI / 180);
                    }
//                    LinearRing circleRing = new LinearRing(circleCoordinates);

                    // Check if the layer is of point type, and check if a point in the layer is within the double-click circle.
                    MapPointLayer thePointLayer = mainMap.Layers[layerNumber] as MapPointLayer;
                    if (thePointLayer != null)
                    {
                        for (int i = 0; i < thePointLayer.DataSet.Vertex.Length; i += 2)
                        {
                            GeoAPI.Geometries.Coordinate pointVertex = new GeoAPI.Geometries.Coordinate(thePointLayer.DataSet.Vertex[i], thePointLayer.DataSet.Vertex[i + 1]);
                            if (CoordinateInPolygon(pointVertex, circleVertices) == true)
                                return true;
                        }

/*                        DotSpatial.Data.IFeatureSet theFeatureSet = thePointLayer.FeatureSet;
                        for (featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)
                        {
                            DotSpatial.Topology.Point thePoint = theFeatureSet.Features[featureNumber].Geometry as DotSpatial.Topology.Point;
                            // Determine if the distance between the feature point and the mouse double-click point is less than the radius
                            double latitudeDistance = (thePoint.Y - theCoordinates.Y) * Math.Cos(theCoordinates.Y * Math.PI / 180);
                            double longitudeDistance = thePoint.X - theCoordinates.X;
                            if (Math.Sqrt(Math.Pow(latitudeDistance, 2) + Math.Pow(longitudeDistance, 2)) <= latitudeRadius)
                                return true;
                        }*/
                    }

                    // Check if the layer is of line type, and check if a line intersects with the double-click circle.
                    MapLineLayer theLineLayer = mainMap.Layers[layerNumber] as MapLineLayer;
                    if (theLineLayer != null)
                    {
                        if (GetLineToPointDistance(theLineLayer, theCoordinates) <= latitudeRadius)
                            return true;
//                        DotSpatial.Data.IFeatureSet theFeatureSet = theLineLayer.FeatureSet;
//                        for (featureNumber = 0; featureNumber < theFeatureSet.Features.Count; featureNumber++)
//                        {
//                            if (theFeatureSet.Features[featureNumber].Intersects(circleRing))
//                                return true;
//                        }
                    }
                }
            }

            return false;
        }
