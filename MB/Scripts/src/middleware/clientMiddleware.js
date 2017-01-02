export default function clientMiddleware(client) {
  return ({dispatch, getState}) => {
    return next => action => {
      if (typeof action === 'function') {
        return action(dispatch, getState);
      }
      const { promise,success, fail,payload,types, ...rest } = action;
      if (!promise) {
        return next(action);
      }
      const [REQUEST, SUCCESS, FAILURE] = types;
      next({...rest, type: REQUEST});
      return promise(client).then(
        (result) => {
          next({...rest, result, payload, type: SUCCESS});
          if (success) {
            success();
          }
        },
        (error) => {
          next({...rest, error, type: FAILURE});
          if (fail) {
            fail();
          }
        }
      ).catch((error)=> {
          console.error('MIDDLEWARE ERROR:', error);
          next({...rest, error, type: FAILURE});
        });
    };
  };
}
