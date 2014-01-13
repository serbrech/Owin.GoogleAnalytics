/*global module:false*/
module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    // Task configuration.
    gruntfile: {
      src: 'Gruntfile.js'
    },
    shell: {
      runtests: {
        options: {                        // Options
          stdout: true
        },
        command: 'sh runtests.sh'
      }
    },
    tests: {
      bin: ['**/bin/Debug/*Tests.dll']
    },
    watch: {
      lib_test: {
        files: ['**/bin/Debug/*Tests.dll'],
        tasks: ['shell:runtests']
      }
    }
  });

  // These plugins provide necessary tasks.
  grunt.loadNpmTasks('grunt-contrib-watch');
  grunt.loadNpmTasks('grunt-shell');

  // Default task.
  grunt.registerTask('default', ['shell:runtests']);

};
