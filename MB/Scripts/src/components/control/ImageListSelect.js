/**
 * Created by Administrator on 2016/8/17 0017.
 */
import React,{PropTypes} from 'react';
import { Checkbox } from 'antd';
import _ from 'lodash';

const ImageListSelect = React.createClass({
  propTypes: {
    images: PropTypes.array
  },

  onItemChange(value){
    this.setState({
      value
    });
    this.props.onChange(value);
  },

  getInitialState(){
    return {
      value: this.props.value
    }
  },

  componentWillReceiveProps(nextProps){
    this.setState({
      value: nextProps.value
    })
  },

  render(){
    const { images }=this.props;
    const { value }=this.state;

    return (
      <div className='image-list'>
        {images.map((img, gIndex)=> {
          const selected = img === value ? 'selected' : ''
          return (
            <div className={`image-item ${selected}`} key={gIndex} onClick={this.onItemChange.bind(this,img)}>
              <img src={img}/>
            </div>
          )
        })}
      </div>
    )
  }
});

export default  ImageListSelect;


