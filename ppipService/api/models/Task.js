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
      type: 'json'
    },

    status: {
      type: 'string',
      in: ['notStarted', 'inProgress', 'succeeded', 'failed'],
      required: true,
      defaultsTo: 'notStarted'
    },

    phone: { // Many to one
      model: 'phone',
      required: true
    },

    pipeline: { // Many to one
      model: 'pipeline',
      required: true
    }

  },

  newUploadTasks: function (pipeline) {
    var tasks = [];
    var pipelineInfo = pipeline.pipelineInfo;

    for (var i = pipeline.input.length - 1; i >= 0; i--) {

      tasks.push({
        type: 'upload',
        taskInfo: {
          storageAccountName: pipelineInfo.storageAccountName,
          storageAccountKey: pipelineInfo.storageAccountKey,
          blobContainer: pipelineInfo.blobContainer
        },
        phone: pipeline.input[0].id,
        pipeline: pipeline
      });

    };

    return tasks;
  },

  newDownloadTasks: function (pipeline, blobName) {
    var tasks = [];
    var pipelineInfo = pipeline.pipelineInfo;

    for (var i = pipeline.output.length - 1; i >= 0; i--) {

      tasks.push({
        type: 'download',
        taskInfo: {
          storageAccountName: pipelineInfo.storageAccountName,
          storageAccountKey: pipelineInfo.storageAccountKey,
          blobContainer: pipelineInfo.blobContainer,
          blobName: blobName
        },
        phone: pipeline.output[i].id,
        pipeline: pipeline
      });

    };

    return tasks;
  }
};

