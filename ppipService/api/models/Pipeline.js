/**
* Pipeline.js
*
* @description :: TODO: You might write a short summary of how this model works and what it represents here.
* @docs        :: http://sailsjs.org/#!documentation/models
*/

module.exports = {

  attributes: {

    name: {
      type: 'string',
      required: true
    },

    type: {
      type: 'string',
      in: ['sharePicture'],
      required: true
    },

    pipelineInfo: {
      type: 'json'
    },

    status: {
      type: 'string',
      in: ['enabled', 'disabled'],
      required: true,
      defaultsTo: 'disabled'
    },

    input: { // Many to many
      collection: 'phone'
    },

    output: { // Many to many
      collection: 'phone'
    }
  }
};

