﻿//-----------------------------------------------------------------------
// <copyright file="UsageExamplesDemo.cs" company="Trimble Inc.">
//     Copyright (c) Trimble Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace CDMServicesUsageExamples
{
    using System;
    using System.Threading.Tasks;

    using Trimble.Connect.Client;
    using Trimble.Connect.Org.Client;
    using Trimble.Connect.PSet.Client;

    /// <summary>
    /// Usage examples demonstration.
    /// This part of the class contains basic functionality.
    /// </summary>
    public partial class UsageExamplesDemo
    {
        /// <summary>
        /// The Organizer service client.
        /// </summary>
        private IOrgClient orgClient;

        /// <summary>
        /// The Property Set service client.
        /// </summary>
        private IPSetClient psetClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsageExamplesDemo"/> class.
        /// </summary>
        /// <param name="credentialsProvider">The credentials provider.</param>
        public UsageExamplesDemo(ICredentialsProvider credentialsProvider)
        {
            if (credentialsProvider is null)
            {
                throw new ArgumentNullException(nameof(credentialsProvider));
            }

            // Create the Organizer service client based on the Organizer service URL specified in the Config
            // and based on the credentials provider that was previously created.
            this.orgClient = new OrgClient(
                new OrgClientConfig { ServiceURI = new Uri(Config.OrgServiceUrl) },
                credentialsProvider);

            // Create the Property Set service client based on the Property Set service URL specified in the Config
            // and based on the credentials provider that was previously created.
            this.psetClient = new PSetClient(
                new PSetClientConfig { ServiceURI = new Uri(Config.PSetServiceUrl) },
                credentialsProvider);
        }

        /// <summary>
        /// The main method demonstrating the usage examples.
        /// </summary>
        /// <returns>Does not return anything.</returns>
        public async Task Run()
        {
            Console.WriteLine("----- Starting the usage examples demo... -----");
            Console.WriteLine();

            Console.WriteLine("Demonstrating how to discover a PSet library based on a project ID and a library name...");
            await LibraryDiscoveryDemo().ConfigureAwait(false);
            Console.WriteLine();

            Console.WriteLine("Demonstrating how to list all the property sets which belong to a specified entity in a specified project...");
            await ListPropertySetsOfEntityDemo().ConfigureAwait(false);
            Console.WriteLine();

            Console.WriteLine("----- Finished the usage examples .NET SDK demo. -----");
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrate how to discover a PSet library based on a project ID and a library name.
        /// </summary>
        /// <returns>Does not return anything.</returns>
        private async Task LibraryDiscoveryDemo()
        {
            Console.Write("Please enter the project ID which contains the PSet library to discover: ");
            string projectId = Console.ReadLine();
            Console.Write("Please enter the name of the PSet library to discover: ");
            string libraryName = Console.ReadLine();

            string libraryId = await this.DiscoverProjectLibrary(projectId, libraryName).ConfigureAwait(false);
            if (string.IsNullOrEmpty(libraryId))
            {
                Console.WriteLine($"No library named {libraryName} exists in the project {projectId}.");
            }
            else
            {
                Console.WriteLine($"The ID of the library named {libraryName} in the project {projectId} is: {libraryId}.");
            }
        }

        /// <summary>
        /// Demonstrate how to list all the property sets of an entity in a project.
        /// </summary>
        /// <returns>Does not return anything.</returns>
        private async Task ListPropertySetsOfEntityDemo()
        {
            Console.Write("Please enter the ID of the project in which the property sets exist: ");
            string projectId = Console.ReadLine();
            Console.Write("Please enter the ID of the entity for which to list the property sets: ");
            string entityID = Console.ReadLine();

            await ListPropertySetsOfEntityInProject(projectId, entityID).ConfigureAwait(false);
        }
    }
}