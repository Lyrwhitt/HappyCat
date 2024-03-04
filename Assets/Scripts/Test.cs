using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{
    public bool testGizmo;
    public Vector3 testGizmoSize;
    public Vector3 testGizmoCenter;
    public Quaternion testGizmoRotation;

    public Test(bool testGizmo, Vector3 testGizmoSize, Vector3 testGizmoCenter, Quaternion testGizmoRotation)
    {
        this.testGizmo = testGizmo;
        this.testGizmoSize = testGizmoSize;
        this.testGizmoCenter = testGizmoCenter;
        this.testGizmoRotation = testGizmoRotation;
    }
}
