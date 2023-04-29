using System;
using System.Numerics;
using Unity.Mathematics;

namespace VectorField {
    using static math;
    using RVector = MathNet.Numerics.LinearAlgebra.Vector<double>;
    using CVector = MathNet.Numerics.LinearAlgebra.Vector<Complex>;
    using RDense = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;
    
    public class StripePattern {
        private readonly HeGeom G;

        public StripePattern(HeGeom g) {
            G = g;
        }

        /*
         * Algorithm 2.
         * Be sure h is inserted only once.
         */ 
        public RVector VertexPolarAngles() {
            var angles = RVector.Build.Dense(G.halfedges.Length);
            foreach (var v in G.Verts) {
                var angle = 0f;
                var proj2plane = 2 * PI / G.AngleSum(v);
                foreach (var h in G.GetAdjacentHalfedges(v)) {
                    var v1 = normalize(G.Vector(h));
                    var v2 = normalize(G.Vector(h.twin.next));
                    angle += acos(dot(v1, v2)) * proj2plane;
                    angles[h.id] = angle;
                }
            }
            return angles;
        }
        
        /*
         * Algorithm 3.
         * X: tangent field in complex number on verts
         * v: target frequency on verts
         */
        public (RVector sign, RVector omega) InitializeEdgeData(RVector angles, CVector X, RVector v) {
            var sign  = RVector.Build.Dense(G.nEdges);
            var omega = RVector.Build.Dense(G.nEdges);
            foreach (var e in G.Edges) {
                var hi = G.halfedges[e.hid];
                var hj = G.halfedges[e.hid].twin;
                var vi = hi.vid;
                var vj = hj.vid;
                var rho_ij = -angles[hi.id] + angles[hj.id] + PI;
                var sgn_ij = math.sign((float)Dot(new Complex(cos(rho_ij), sin(rho_ij)) * X[vi], X[vj]));
                var phi_i = 0; // arg of X[vi];
                var phi_j = 0; // arg of sgn_ij * X[vj];
                var l = G.Length(G.halfedges[e.hid]);
                var omega_ij = l * 0.5 * (
                    v[vi] * cos(phi_i - angles[hi.id]) +
                    v[vj] * cos(phi_j - angles[hj.id])
                );
                sign[e.eid] = sgn_ij;
                omega[e.eid] = omega_ij;
            }
            return (sign, omega);
        }

        double Dot(Complex a, Complex b) {
            return a.Real * b.Real - a.Imaginary * b.Imaginary;
        }
        
        /*
         * Algrithm 4.
         */
        public RDense ComputeEnegyMatrix(RVector omega, RVector sign) {
            throw new Exception();
        }
    }
}
