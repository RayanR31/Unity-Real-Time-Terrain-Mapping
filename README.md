# Unity-Real-Time-Terrain-Mapping

A Unity prototype demonstrating real-time terrain scanning and reconstruction.

The system continuously scans the environment using radial raycasts and progressively builds a runtime map of the explored area.

Features
Real-time terrain scanning
Radial raycast acquisition
Runtime map generation
Procedural mesh reconstruction
Terrain slope analysis
Runtime visualization overlay
Autonomous probe exploration

Architecture

Probe -> ProbeScanner -> ScanData -> RuntimeMapSystem -> RuntimeMapRenderer -> Visualization

The generated map stores:

Terrain height
Surface normals
Explored areas
Traversability information

Color coding:

  Green → flat terrain
  Yellow → moderate slope
  Red → steep terrain
  Black → unexplored area
  
Technologies

  Unity
  C#
  Procedural Mesh Generation
  Runtime Data Processing
  Physics Raycasting
  Future Improvements
  Multi-probe support
  Jobs / Burst optimization
  Terrain classification
  Path planning
  Occupancy grid generation
