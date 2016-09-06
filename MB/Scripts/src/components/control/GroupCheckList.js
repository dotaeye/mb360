/**
 * Created by Administrator on 2016/8/17 0017.
 */
import React,{PropTypes} from 'react';
import { Checkbox } from 'antd';
import _ from 'lodash';
const CheckboxGroup = Checkbox.Group;

const GroupCheckList = React.createClass({
  propTypes: {
    groups: PropTypes.array
  },

  onGroupItemChange(gIndex, values){
    const currentGroup = this.props.groups[gIndex];
    const { value }=this.state;
    currentGroup.children.forEach(item=> {
      if (value.indexOf(item.value) != -1) {
        if (values.indexOf(item.value) == -1) {
          value.splice(value.indexOf(item.value), 1);
        }
      } else {
        if (values.indexOf(item.value) != -1) {
          value.push(item.value);
        }
      }
    });
    this.setState({
      value
    });
    this.props.onChange(value);
  },

  onGroupSelectAll(gIndex, values, event){
    this.onGroupItemChange(gIndex, event.target.checked ? values : []);
  },

  getInitialState(){
    return {
      value: this.props.value || []
    }
  },

  componentWillReceiveProps(nextProps){
    this.setState({
      value: nextProps.value || []
    })
  },

  render(){
    const { groups }=this.props;
    const { value }=this.state;

    return (
      <div className='group-check-list'>
        {groups.map((group, gIndex)=> {
          const groupValues = _.map(group.children, 'value');
          const selectedValues = [];
          groupValues.forEach(val=> {
            if (value.indexOf(val) > -1) {
              selectedValues.push(val);
            }
          });
          const isGroupChecked = selectedValues.length === groupValues.length;
          return (
            <div className='group-check-item' key={gIndex}>
              <label className='group-check-label'>
                <Checkbox
                  title='全选/反选'
                  onChange={this.onGroupSelectAll.bind(this,gIndex, groupValues)} checked={isGroupChecked}>{group.name}</Checkbox></label>

              <div className='group-check-select'>
                <CheckboxGroup options={group.children}
                               value={selectedValues}
                               onChange={this.onGroupItemChange.bind(this,gIndex)}/>
              </div>
            </div>
          )
        })}
      </div>
    )
  }
});

export default  GroupCheckList;


