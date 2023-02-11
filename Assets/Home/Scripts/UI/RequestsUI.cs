using PolyAndCode.UI;
using Rover.Core.Objects;
using Rover.Core.Record;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rover.Home.UI
{
    [AddComponentMenu("Rover/Home/UI/Requests UI")]
    public class RequestsUI : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        #region Properties and Fields

        [Header("Data")]
        [SerializeField] private RequestRecord requestRecord;

        [Header("Items")]
        [SerializeField] private RecyclableScrollRect scrollRect;

        [NonSerialized] private List<RequestUIData> requestUIData = new List<RequestUIData>();

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            RefreshUI();
        }

        private void OnDisable()
        {
            requestUIData.Clear();
        }

        #endregion

        private void RefreshUI()
        {
            requestUIData.Clear();

            for (int i = 0, n = requestRecord.NumPendingRequests; i < n; ++i)
            {
                Request request = requestRecord.GetRequest(i);
                requestUIData.Add(new RequestUIData(request));
            }

            scrollRect.DataSource = this;
            scrollRect.ReloadData();
        }

        #region Data Source Methods

        public int GetItemCount()
        {
            return requestUIData.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            RequestUI requestUI = cell as RequestUI;
            requestUI.ConfigureCell(requestUIData[index], index);
        }

        #endregion

        #region Callbacks

        public void OnRequestRecordChanged()
        {
            RefreshUI();
        }

        #endregion
    }
}
