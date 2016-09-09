/**
 * Created by Administrator on 2016/8/17 0017.
 */
import React,{PropTypes} from 'react';
import { Input,Form } from 'antd';
import _ from 'lodash';
const FormItem = Form.Item;

const MapMaker = React.createClass({

  propTypes: {
    latitude: PropTypes.number,
    longitude: PropTypes.number,
    address: PropTypes.string,
    setLocation: PropTypes.bool,
  },

  componentDidMount(){
    window.mapLoad = this.mapLoad;
    this.loadScript()
  },

  setDefaultLocation(props){
    const {latitude, longitude, address}=props;
    var point;
    if (latitude && longitude) {
      point = new BMap.Point(longitude, latitude);
    }
    this.map.centerAndZoom(point ? point : address ? address : '北京', 16);
    this.serachList.setInputValue(address ? address : '北京');
  },

  componentWillReceiveProps(nextProps){
    if(nextProps.setLocation){
      console.log('componentWillReceiveProps');
      console.log(nextProps.address);
      this.setDefaultLocation(nextProps);
    }
  },

  mapLoad(){
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

      }.bind(this));
      this.serachList.setInputValue(this.props.address);
    }
    this.setDefaultLocation(this.props);
  },

  onSearchComplete(address){
    const infoOptions = {
      width: 100,     // 信息窗口宽度
      height: 30,     // 信息窗口高度
      title: "提示："  // 信息窗口标题
    };
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
      this.props.onChange(state);
    });
  },

  loadScript(){
    var script = document.createElement("script");
    script.src = "http://api.map.baidu.com/api?v=2.0&ak=bdk7ILdPkvGgiKTiutGWR08lk2rxHZZL&callback=mapLoad";
    document.body.appendChild(script);
  },

  render(){

    return (
      <div className='map-maker'>
        <Input id='map-search' ref={(ref)=>this.searchBox=ref} className='ant-input-lg' type='text'/>

        <div className='map-maker-container' id='map-maker'>
        </div>
      </div>
    )
  }
});

export default  MapMaker;


