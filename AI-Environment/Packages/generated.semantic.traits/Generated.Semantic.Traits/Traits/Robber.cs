using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Semantic.Traits;
using Unity.Entities;
using UnityEngine;

namespace Generated.Semantic.Traits
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    [AddComponentMenu("Semantic/Traits/Robber (Trait)")]
    [RequireComponent(typeof(SemanticObject))]
    public partial class Robber : MonoBehaviour, ITrait
    {
        public System.Boolean Ready2steal
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity))
                {
                    m_p0 = m_EntityManager.GetComponentData<RobberData>(m_Entity).Ready2steal;
                }

                return m_p0;
            }
            set
            {
                RobberData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<RobberData>(m_Entity);
                data.Ready2steal = m_p0 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Boolean Stolen
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity))
                {
                    m_p1 = m_EntityManager.GetComponentData<RobberData>(m_Entity).Stolen;
                }

                return m_p1;
            }
            set
            {
                RobberData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<RobberData>(m_Entity);
                data.Stolen = m_p1 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public System.Boolean CopAway
        {
            get
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity))
                {
                    m_p2 = m_EntityManager.GetComponentData<RobberData>(m_Entity).CopAway;
                }

                return m_p2;
            }
            set
            {
                RobberData data = default;
                var dataActive = m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity);
                if (dataActive)
                    data = m_EntityManager.GetComponentData<RobberData>(m_Entity);
                data.CopAway = m_p2 = value;
                if (dataActive)
                    m_EntityManager.SetComponentData(m_Entity, data);
            }
        }
        public RobberData Data
        {
            get => m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity) ?
                m_EntityManager.GetComponentData<RobberData>(m_Entity) : GetData();
            set
            {
                if (m_EntityManager != default && m_EntityManager.HasComponent<RobberData>(m_Entity))
                    m_EntityManager.SetComponentData(m_Entity, value);
            }
        }

        #pragma warning disable 649
        [SerializeField]
        [InspectorName("Ready2steal")]
        System.Boolean m_p0 = false;
        [SerializeField]
        [InspectorName("Stolen")]
        System.Boolean m_p1 = false;
        [SerializeField]
        [InspectorName("CopAway")]
        System.Boolean m_p2 = false;
        #pragma warning restore 649

        EntityManager m_EntityManager;
        World m_World;
        Entity m_Entity;

        RobberData GetData()
        {
            RobberData data = default;
            data.Ready2steal = m_p0;
            data.Stolen = m_p1;
            data.CopAway = m_p2;

            return data;
        }

        
        void OnEnable()
        {
            // Handle the case where this trait is added after conversion
            var semanticObject = GetComponent<SemanticObject>();
            if (semanticObject && !semanticObject.Entity.Equals(default))
                Convert(semanticObject.Entity, semanticObject.EntityManager, null);
        }

        public void Convert(Entity entity, EntityManager destinationManager, GameObjectConversionSystem _)
        {
            m_Entity = entity;
            m_EntityManager = destinationManager;
            m_World = destinationManager.World;

            if (!destinationManager.HasComponent(entity, typeof(RobberData)))
            {
                destinationManager.AddComponentData(entity, GetData());
            }
        }

        void OnDestroy()
        {
            if (m_World != default && m_World.IsCreated)
            {
                m_EntityManager.RemoveComponent<RobberData>(m_Entity);
                if (m_EntityManager.GetComponentCount(m_Entity) == 0)
                    m_EntityManager.DestroyEntity(m_Entity);
            }
        }

        void OnValidate()
        {

            // Commit local fields to backing store
            Data = GetData();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            TraitGizmos.DrawGizmoForTrait(nameof(RobberData), gameObject,Data);
        }
#endif
    }
}
