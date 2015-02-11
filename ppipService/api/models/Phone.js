/**
* Phone.js
*
* @description :: TODO: You might write a short summary of how this model works and what it represents here.
* @docs        :: http://sailsjs.org/#!documentation/models
*/

module.exports = {

  autosubscribe: ['create', 'destroy', 'update', 'add:tasks', 'remove:tasks'],

  attributes: {

    name: {
      type: 'string',
      required: true
    },

    deviceName: {
      type: 'string'
    },

    status: {
      type: 'string',
      in: ['offline', 'online'],
      required: true,
      defaultsTo: 'offline'
    },

    token: {
      type: 'string'
    },

    tasks: { // One to many
      collection: 'task',
      via: 'phone'
    }

  }
};

