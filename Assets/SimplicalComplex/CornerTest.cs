using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ddg {
    public class CornerTest : MonoBehaviour {
        [SerializeField] int id;
        HalfEdgeMesh mesh;
        GameObject cube1;
        int count = 0;

        /*
        void Start() {
            var m = GetComponentInChildren<MeshFilter>().sharedMesh;
            mesh = new HalfEdgeMesh(m);
            cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube1.transform.localScale *= 0.1f;
            StartCoroutine(CornerCoroutine(id));
        }

        IEnumerator CornerCoroutine(int id) {
            var hes = mesh.halfedges;
            yield return null;
            foreach(var c in mesh.Verts[hes[id].vid].GetAdjacentConers(hes)) {
            //foreach(var c in hes[id].vert.GetAdjacentConers(hes)) {
                cube1.transform.position = mesh.Pos[hes[c.hid].vid];
                yield return new WaitForSeconds(0.5f);
            }
        }
        */
    }
}

