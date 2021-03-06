﻿/**
 * Created by Administrator on 2016/8/17 0017.
 */
import React,{PropTypes} from 'react';
import { Input, Form, Spin } from 'antd';
import _ from 'lodash';
const FormItem = Form.Item;

const MapMaker = React.createClass({

  getInitialState(){
    return {
      value: this.props.value || {},
      loading: true
    }
  },

  componentWillReceiveProps(nextProps){
    this.setState({
      value: nextProps.value || {}
    })
  },

  componentDidMount(){
    window.mapLoad = this.mapLoad;
    if (typeof BMap === "undefined") {
      this.loadScript()
    } else {
      this.mapLoad();
    }
  },

  setDefaultLocation(){
    const { value:{ latitude, longitude, address }}=this.state;
    var point;
    if (latitude && longitude) {
      point = new BMap.Point(longitude, latitude);
    }
    this.map.centerAndZoom(point ? point : address ? address : '上海', 18);
    this.serachList.setInputValue(address ? address : '上海');

  },

  resetMap(){
    this.setState({
      value: {}
    })
  },

  mapLoad(){
    this.setState({
      loading: false
    });
    this.map = new BMap.Map('map-maker');
    this.geoc = new BMap.Geocoder();
    if (!this.serachList) {
      this.serachList = new BMap.Autocomplete({
        "input": "map-search",
        "location": this.map
      });
      this.serachList.addEventListener("onconfirm", function (e) {    //鼠标点击下拉列表后的事件
        const _value = e.item.value;
        const address = _value.province + _value.city + _value.district + _value.street + _value.business;
        this.map.clearOverlays();
        this.local = new BMap.LocalSearch(this.map, { //智能搜索
          onSearchComplete: this.onSearchComplete.bind(this, address)
        });
        this.local.search(address);
        this.setState(Object.assign({}, this.state.value, {address}))
      }.bind(this));
    }
    this.setDefaultLocation();
  },

  onSearchComplete(address){
    const infoWinContent = "拖动图标到您需要标记的位置!";
    const pp = this.local.getResults().getPoi(0).point;    //获取第一个智能搜索的结果
    this.map.centerAndZoom(pp, 18);
    const myIcon = new BMap.Icon("http://api.map.baidu.com/img/markers.png", new BMap.Size(23, 25), {
      offset: new BMap.Size(0, 0),
      imageOffset: new BMap.Size(0, 0 - 10 * 25)
    });
    const marker = new BMap.Marker(pp, {icon: myIcon});
    const infoWindow = new BMap.InfoWindow(infoWinContent, {});  // 创建信息窗口对象
    this.map.addOverlay(marker);

    marker.enableDragging();
    marker.addEventListener("mouseover", function () {
      if (!this.firstOpen) {
        this.firstOpen = true;
        this.openInfoWindow(infoWindow);      // 打开信息窗口
      }
    });
    marker.addEventListener("dragend", function (e) {
      this.getLocation(e.point);
    }.bind(this));
    let state = {
      latitude: pp.lat,
      longitude: pp.lng,
      address
    };
    this.setState(state);
    this.props.onChange(state);
  },

  getLocation(point){
    this.geoc.getLocation(point, function (rs) {
      let addComp = rs.addressComponents;
      let address = addComp.province + addComp.city + addComp.district + addComp.street + addComp.streetNumber;
      let state = {
        latitude: point.lat,
        longitude: point.lng,
        address
      };
      this.setState(state);
      this.props.onChange(state);
      this.serachList.setInputValue(address);
    }.bind(this));
  },

  loadScript(){
    var script = document.createElement("script");
    script.src = "http://api.map.baidu.com/api?v=2.0&ak=bdk7ILdPkvGgiKTiutGWR08lk2rxHZZL&callback=mapLoad";
    document.body.appendChild(script);
  },

  render(){
    const { loading } = this.state;
    return (
      <div className='map-maker'>
        <Input id='map-search' ref={(ref)=>this.searchBox=ref} className='ant-input-lg' type='text'/>
        <Spin spinning={this.state.loading}>
          <div className='map-maker-container' id='map-maker'>
          </div>
        </Spin>
      </div>
    )
  }
});

export default  MapMaker;


