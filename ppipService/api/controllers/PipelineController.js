/**
 * PipelineController
 *
 * @description :: Server-side logic for managing pipelines
 * @help        :: See http://links.sailsjs.org/docs/controllers
 */

module.exports = {

  enable: function (req, res) {
    var id = req.param('id');

    if (!id) {
      return res.missingFields(['id']);
    }

    Pipeline.findOne({
      id: id,
      status: 'disabled'
    })
    .populate('input')
    .populate('output')
    .exec(function (err, pipeline) {
      if (err) {
        return res.serverError(err);
      }

      if (!pipeline){
        return res.notFound();
      }

      if (pipeline.type == 'sharePicture') {

        sails.log.debug(id);

        pipeline.status = 'enabled';
        pipeline.pipelineInfo = {
          storageAccountName: 'testpipeline',
          storageAccountKey: 'cLv4V4nFF2kIyGda9Alu6mOtDspCUsPvPmdtbxpe6XjDxJXNDlfAtW2KlbW1tzw6amAf+mv4WXolbnps1HO4mg==',
          blobContainer: 'ppip'
        }

        return pipeline.save(function (err, pipeline) {
          if (err) {
            return res.serverError(err);
          }

          sails.log.debug(pipeline);

          Task.create(Task.newUploadTasks(pipeline))
          .exec(function (err, tasks) {
            if (err) {
              return res.serverError(err);
            }

            return res.status(200).json(tasks);
          });
        });
      }

      return res.serverError('not supported pipeline type');
    })
  }

};