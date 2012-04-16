﻿using UnityEngine;
using System.Collections.Generic;

namespace Thesis {
namespace Base {

public sealed class DoorBody : ComponentBody
{
  /*************** CONSTRUCTORS ***************/

  public DoorBody (Door parent)
    : base(parent, "door_body")
  {
    material = Resources.Load("Materials/DoorBody", typeof(Material)) as Material;
    active = true;
  }
}

} // namespace Base
} // namespace Thesis