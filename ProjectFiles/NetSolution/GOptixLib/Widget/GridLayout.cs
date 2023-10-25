#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.EventLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.Alarm;
using FTOptix.CommunicationDriver;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Core;
#endregion
using System.Linq;
using System.Collections.Generic;

namespace GOptixLib.Widget;

    public class GridLayout : IDisposable
    {
        List<MapperModelAndUI> mapper = new List<MapperModelAndUI>();
        ModelEventObserver _observer;

        Item _container;
        IUANode _model;
        IUANode _uitype;

        LayoutConfigure _configure;
        public GridLayout(LayoutConfigure configure, Item container, IUANode model, IUANode uitype, IUAObject logicObject)
        {
            _configure = configure;
            _container = container;
            _model = model;
            _uitype = uitype;

            _observer = model.RegisterAddAndRemoveObserver(
              logicObject
              , ModelObserverOnAdded_Handle
              , ModelObserverOnRemoved_Handle
      );
        }

        public void Dispose()
        {
            _observer.Dispose();
        }


        public void Clear()
        {
            foreach (var ui in _container.Children.OfType<Item>())
            {
                ui.Delete();
            }



            mapper.Clear();
        }

        public void BuildUI()
        {

            Clear();

            if (_configure.FillMode)
            {
                _container.VerticalAlignment = VerticalAlignment.Stretch;
            }

            var idx = 0;
            var row = 0;
            var col = 0;
            Item layout = null;

            foreach (var model in _model.Children)
            {
                row = idx / _configure.ColCount;
                col = idx % _configure.ColCount;

                if (col == 0)
                {
                    layout = BuildRowLayout();
                    AddLayout(layout, row);
                }

                var ui = BuildUIWidget(model);
                if (layout != null)
                {
                    AddWidget(layout, ui, model, row, col);

                }
                else
                {
                    throw new Exception("Grid Layout error, layout is null");
                }

                idx++;

            }


            for (var i = col + 1; i < _configure.ColCount; i++)
            {
                AddPlaceholder(layout, row, i);
            }



        }

        public void ModelObserverOnAdded_Handle(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
        {
            var model = targetNode;

            Item layout = null;
            int row, col;
            if (_container.Children.Count <= 0)
            {
                layout = BuildRowLayout();
                AddLayout(layout, 0);
                row = 0;
            }
            else
            {

                var maxRow = mapper.Select(m => m.Row).Max();
                var count = DeletePlaceholders(maxRow);
                if (count == 0)
                {
                    layout = BuildRowLayout();
                    AddLayout(layout, maxRow + 1);
                    row = maxRow + 1;
                }
                else
                {
                    row = maxRow;
                    var _mapper = FindLayout(maxRow);
                    if (_mapper == null)
                    {
                        layout = BuildRowLayout();
                        AddLayout(layout, maxRow);
                    }
                    else
                    {
                        layout = _mapper.UI as Item;
                    }
                }
            }

            col = layout.Children.OfType<Item>().Count();


            var ui = BuildUIWidget(model);
            if (layout != null)
            {
                AddWidget(layout, ui, model, row, col);

            }
            else
            {
                throw new Exception("Grid Layout error, layout is null");
            }

            for (var i = col + 1; i < _configure.ColCount; i++)
            {
                AddPlaceholder(layout, row, i);
            }



        }

        public void ModelObserverOnRemoved_Handle(IUANode sourceNode, IUANode targetNode, NodeId referenceTypeId, ulong senderId)
        {
            //var model = targetNode;
            //var _mapper = mapper.Where(m=>m.UIType == WidgetType.Widget && m.Model == model).FirstOrDefault();
            BuildUI();

        }

        private MapperModelAndUI FindLayout(int row)
        {
            var layout = mapper.Where(m => m.UIType == WidgetType.Layout && m.Row == row).FirstOrDefault();
            return layout;

        }

        private IEnumerable<IUANode> FindPlaceholder(int row)
        {
            var placeholders = mapper.Where(m => m.UIType == WidgetType.Placeholder && m.Row == row).Select(m1 => m1.UI);
            return placeholders;
        }


        private int DeletePlaceholders(int row)
        {

            var uis = FindPlaceholder(row).ToList();
            var count = mapper.RemoveAll(m => m.UIType == WidgetType.Placeholder && m.Row == row);
            foreach (var ui in uis)
            {
                ui.Delete();
            }

            return count;
        }


        private void AddLayout(Item layout, int row)
        {
            _container.Add(layout);
            mapper.Add(new MapperModelAndUI() { UI = layout, Row = row, UIType = WidgetType.Layout });
        }

        private void AddWidget(Item layout, Item widget, IUANode model, int row, int col)
        {
            layout.Add(widget);
            mapper.Add(new MapperModelAndUI() { UI = widget, Model = model, Row = row, Col = col, UIType = WidgetType.Widget });
        }

        private void AddPlaceholder(Item layout, int row, int col)
        {
            var ui = BuildPlaceHolderPanel();
            layout.Add(ui);
            mapper.Add(new MapperModelAndUI() { UI = ui, Row = row, Col = col, UIType = WidgetType.Placeholder });
        }

        private Item BuildRowLayout()
        {
            var name = Guid.NewGuid().ToString();

            var hl = InformationModel.MakeObject<RowLayout>(name);
            hl.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (!_configure.FillMode)
            {

                hl.VerticalAlignment = VerticalAlignment.Top;
                hl.Height = _configure.RowHeight;
            }
            else
            {
                hl.VerticalAlignment = VerticalAlignment.Stretch;
            }
            return hl;
        }


        private Item BuildPlaceHolderPanel()
        {

            var name = Guid.NewGuid().ToString();

            var ui = InformationModel.MakeObject<Panel>(name) as Item;

            ui.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (_configure.FillMode)
            {
                ui.VerticalAlignment = VerticalAlignment.Stretch;
            }
            ui.TopMargin = _configure.RowOffset;
            ui.BottomMargin = _configure.RowOffset;
            ui.LeftMargin = _configure.ColOffset;
            ui.RightMargin = _configure.ColOffset;

            return ui;
        }


        private Item BuildUIWidget(IUANode model)
        {

            var name = Guid.NewGuid().ToString();


            var ui = InformationModel.MakeObject(model.BrowseName, _uitype.NodeId) as Item;
            if (ui == null)
            {
                return null;
            }

            ui.HorizontalAlignment = HorizontalAlignment.Stretch;
            if (_configure.FillMode)
            {
                ui.VerticalAlignment = VerticalAlignment.Stretch;
            }
            ui.TopMargin = _configure.RowOffset;
            ui.BottomMargin = _configure.RowOffset;
            ui.LeftMargin = _configure.ColOffset;
            ui.RightMargin = _configure.ColOffset;

            foreach (var child in ui.Children)
            {
                if (child.GetType() == typeof(Alias))
                {
                    ui.SetAlias(child.BrowseName, model);
                    break;
                }
            }

            return ui;

        }


    }


    public class LayoutConfigure
    {
        public bool FillMode { get; set; }
        public float RowOffset { get; set; }
        public float RowHeight { get; set; }
        public float ColOffset { get; set; }
        public int ColCount { get; set; }

    }

    internal class MapperModelAndUI
    {
        public IUANode Model { get; set; }
        public IUANode UI { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public WidgetType UIType { get; set; }

    }


    internal enum WidgetType
    {
        Layout,
        Widget,
        Placeholder
    }
