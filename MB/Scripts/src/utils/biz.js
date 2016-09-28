/**
 * Created by Administrator on 2016/8/25 0025.
 */
import _ from 'lodash';

export function getGroupSelectData(data, groupBy) {
  let groups = [];
  let temp = _.groupBy(data, groupBy);
  _.map(temp, (groupItems, groupKey)=> {
    let groupItem = {
      name: groupKey,
      children: []
    };
    groupItem.children = groupItems.map(x=> {
      return {
        label: x.name,
        value: x.id
      }
    });


    groups.push(groupItem);
  });
  return groups;
}

export function hasError(error) {
  if (_.isArray(error)) {
    return _.some(error)
  }
  return error;
}


export function setCascadeValues(options, value, results) {
  results.push(value.toString());
  let option = getSelectedOption(options, value);
  if (option && option.parentId) {
    setCascadeValues(options, option.parentId, results)
  }
}

function getSelectedOption(options, value) {
  var result = null;
  for (let i = 0; i < options.length; i++) {
    if (options[i].value == value) {
      result = options[i];
    } else {
      if (options[i].children && options[i].children.length > 0) {
        var op = getSelectedOption(options[i].children, value);
        if (op) {
          result = op
        }
      }
    }
  }
  return result;
}

export function checkLocation(rule, value, callback) {
  if (value && value.address && value.latitude && value.longitude) {
    callback();
  } else {
    callback(new Error('请标记一个具体位置'));
  }
}

export function guid() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}

export function getCascaderName(ids, cascader) {
  let currentCascader = cascader;
  let results = [];
  ids.forEach(id=> {
    let currentNode = currentCascader.find(c=>c.value == id);
    results.push(currentNode.label);
    currentCascader = currentNode.children;
  })
  return results;
}