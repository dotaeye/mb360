const isEmpty = value => value === undefined || value === null || value === '';
const join = (rules) => (value, data, key) => rules.map(rule => rule(value, data, key)).filter(error => !!error)[0 /* first error */];

export function email(value, data, displayName) {
  // Let's not start a debate on email regex. This is just for an example app!
  if (!isEmpty(value) && !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(value)) {
    return `${displayName}必须为正确的邮箱地址`;
  }
}

export function required(value, data, displayName) {
  if (isEmpty(value)) {
    return `${displayName}不允许为空`;
  }
}

export function equalTo(comparefield, errorMessage) {
  return (value, data, displayName)=> {
    if (value !== data[comparefield]) {
      return errorMessage ? errorMessage : `两次密码输入不一致`
    }
  }
}

export function minLength(min) {
  return (value, data, displayName) => {
    if (!isEmpty(value) && value.length < min) {
      return `${displayName}至少为${min}个字符`;
    }
  };
}

export function maxLength(max) {
  return (value, data, displayName) => {
    if (!isEmpty(value) && value.length > max) {
      return `${displayName}不能超过${min}个字符`;
    }
  };
}

export function integer(value) {
  if (!Number.isInteger(Number(value))) {
    return `${displayName}必须为整数`;
  }
}

export function oneOf(enumeration) {
  return (value, data, displayName) => {
    if (!~enumeration.indexOf(value)) {
      return `${displayName}必须为 ${enumeration.join(', ')} 其中的一个`;
    }
  };
}

export function match(field) {
  return (value, data) => {
    if (data) {
      if (value !== data[field]) {
        return 'Do not match';
      }
    }
  };
}


export function createValidator(options) {
  return (data = {}) => {
    const errors = {};
    Object.keys(options).forEach((key) => {
      const rule = join([].concat(options[key].rules)); // concat enables both functions and arrays of functions
      const error = rule(data[key], data, options[key].displayName);
      if (error) {
        errors[key] = error;
      }
    });
    return errors;
  };
}
