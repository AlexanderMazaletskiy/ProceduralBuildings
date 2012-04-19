﻿using UnityEngine;

namespace Thesis {
namespace Base {

public class BalconyFloor : DrawableObject
{
  /*************** FIELDS ***************/

  public readonly BalconyDoor parentBalconyDoor;

  public float height;

  public float width;

  public float depth;

  public Neoclassical parentBuilding
  {
    get { return (Neoclassical) parentBalconyDoor.parentBuilding; }
  }

  /*************** CONSTRUCTORS ***************/

  public BalconyFloor (BalconyDoor parent)
    : base("balcony_floor", "Building")
  {
    parentBalconyDoor = parent;

    height = parentBuilding.balconyFloorHeight;
    width = parentBuilding.balconyFloorWidth;
    depth = parentBuilding.balconyFloorDepth;

    var tmp_right = parentBalconyDoor.parentFace.right * width;
    var tmp_normal = parentBalconyDoor.parentFace.normal * depth;

    boundaries = new Vector3[8];
    boundaries[0] = parentBalconyDoor.boundaries[1] - tmp_right + tmp_normal;
    boundaries[1] = parentBalconyDoor.boundaries[1] - tmp_right - tmp_normal;
    boundaries[2] = parentBalconyDoor.boundaries[0] + tmp_right - tmp_normal;
    boundaries[3] = parentBalconyDoor.boundaries[0] + tmp_right + tmp_normal;

    for (var i = 0; i < 4; ++i)
      boundaries[i + 4] = boundaries[i] + Vector3.up * height;

    FindMeshOrigin(boundaries[0],
                   boundaries[6],
                   boundaries[2],
                   boundaries[4]);

    for (var i = 0; i < boundaries.Length; ++i)
      boundaries[i] -= meshOrigin;
  }

  public override void FindVertices ()
  {
    vertices = new Vector3[boundaries.Length * 3];
    for (int i = 0; i < 3; ++i)
      System.Array.Copy(boundaries, 0, vertices, i * boundaries.Length, boundaries.Length);
  }

  public override void FindTriangles ()
  {
    triangles = new int[36];

    // bottom
    triangles[0]  = 0;
    triangles[1]  = 2;
    triangles[2]  = 1;

    triangles[3]  = 0;
    triangles[4]  = 3;
    triangles[5]  = 2;

    // top
    triangles[6]  = 4;
    triangles[7]  = 5;
    triangles[8]  = 6;

    triangles[9]  = 4;
    triangles[10] = 6;
    triangles[11] = 7;

    // left
    triangles[12] = 9;    // 1 + 8
    triangles[13] = 10;   // 2 + 8
    triangles[14] = 13;   // 5 + 8

    triangles[15] = 10;   // 2 + 8
    triangles[16] = 14;   // 6 + 8
    triangles[17] = 13;   // 5 + 8

    // right
    triangles[18] = 8;    // 0 + 8
    triangles[19] = 12;   // 4 + 8
    triangles[20] = 11;   // 3 + 8

    triangles[21] = 11;   // 3 + 8
    triangles[22] = 12;   // 4 + 8
    triangles[23] = 15;   // 7 + 8

    // front
    triangles[24] = 16;   // 0 + 16
    triangles[25] = 17;   // 1 + 16
    triangles[26] = 20;   // 4 + 16

    triangles[27] = 17;   // 1 + 16
    triangles[28] = 21;   // 5 + 16
    triangles[29] = 20;   // 4 + 16

    // back
    triangles[30] = 18;   // 2 + 16
    triangles[31] = 19;   // 3 + 16
    triangles[32] = 23;   // 7 + 16

    triangles[33] = 18;   // 2 + 16
    triangles[34] = 23;   // 7 + 16
    triangles[35] = 22;   // 6 + 16
  }

  public override void Draw ()
  {
    base.Draw();

    gameObject.transform.position = meshOrigin;
    gameObject.transform.parent = parentBuilding.gameObject.transform;
  }
}

} // namespace Base
} // namespace Thesis