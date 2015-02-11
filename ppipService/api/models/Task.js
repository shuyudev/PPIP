/**
* Task.js
*
* @description :: TODO: You might write a short summary of how this model works and what it represents here.
* @docs        :: http://sailsjs.org/#!documentation/models
*/

module.exports = {

  attributes: {

    type: {
      type: 'string',
      in: ['upload', 'download'],
      required: true
    },

    taskInfo: {
      type: 'string'
    },

    phone: { // Many to one
      model: 'phone',
      required: true
    }

  }
};

