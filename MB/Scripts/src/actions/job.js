﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


import * as jobTypes from '../contants/job';

export function getAll() {
  return {
    types: [jobTypes.GET_ALL_JOB, jobTypes.GET_ALL_JOB_SUCCESS, jobTypes.GET_ALL_JOB_FAIL],
    promise: (client) => client.get('/job',{
      token: true
    })
  };
}

export function getById(id) {
  return {
    types: [jobTypes.GET_ONE_JOB, jobTypes.GET_ONE_JOB_SUCCESS, jobTypes.GET_ONE_JOB_FAIL],
    promise: (client) => client.get('/job'+id,{
      token: true
    })
  };
}

export function create(data) {
  return {
    types: [job.CREATE_JOB, job.CREATE_JOB_SUCCESS, job.CREATE_JOB_FAIL],
    promise: (client) => client.post('/job', {
      data: data,
	  token: true
    })
  };
}

export function update(id, data) {
  return {
    types: [job.UPDATE_JOB, job.UPDATE_JOB_SUCCESS, job.UPDATE_JOB_FAIL],
    promise: (client) => client.put('/job/'+id, {
      data: data,
	  token: true
    })
  };
}

export function delete(id) {
  return {
    types: [job.DELETE_JOB, job.DELETE_JOB_SUCCESS, job.DELETE_JOB_FAIL],
    promise: (client) => client.delete('/job/'+id, {
		token: true
    })
  };
}

