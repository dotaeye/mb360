import env from './env';

class FakeStorage {
  initialize() {
    this.store = {};
    this.length = 0;
  }

  clear() {
  }

  getItem() {
    return null;
  }

  removeItem() {
  }

  setItem() {
  }
}

var baseStorage = {};
var stringify = JSON.stringify;
var parse = JSON.parse;

if (env.CLIENT) {
  baseStorage = window.Storage;
}
else {
  baseStorage = FakeStorage;
}

baseStorage.prototype.set = function (key, value, expired) {
  var wrapped = {
    _data: value
  };
  if (expired) {
    wrapped.expired = (new Date().addMinutes(expired)).getTime();
  }
  this.setItem(this.namespace + '_' + key, stringify(wrapped));
};

baseStorage.prototype.get = function (key) {
  var string = this.getItem(this.namespace + '_' + key);
  var wrapped = parse(string);
  var result = null;
  if (wrapped) {
    if (this._expired(wrapped)) {
      // remove expired item
      this.removeItem(this.namespace + '_' + key);
    }
    else {
      result = wrapped._data;
    }
  }
  return result;
};

baseStorage.prototype.remove = function (key) {
  this.removeItem(this.namespace + '_' + key);
};

baseStorage.prototype.retrieve = function (key, expired, success, fail) {
  const self = this;
  const data = this.get(key);
  const saveOpts = {
    key: key,
    expired: expired
  };
  if (data) {
    success(data, saveOpts); // true means isCache
  } else {
    fail(function (res) {
      if (res) {
        self.set(key, res, expired);
      }
    }, saveOpts);
  }
};


baseStorage.prototype._expired = function (wrapped) {
  var currentTime = (new Date()).getTime();

  if (wrapped.expired) {
    if (currentTime > wrapped.expired) {
      return true;
    }
  }
  return false;
};

baseStorage.setNamespace = function (namespace) {
  baseStorage.prototype.namespace = namespace;
};

baseStorage.getStorage = function (name) {
  if(name==='session'){
    return sessionStorage;
  }
  return localStorage;
};

export default baseStorage;