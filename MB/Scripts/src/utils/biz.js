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
      items: []
    };
    groupItem.items = groupItems.map(x=> {
      return {
        label: x.name,
        value: x.id
      }
    });
    groups.push(groupItem);
  })
  return groups;
}