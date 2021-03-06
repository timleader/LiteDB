﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace LiteDB
{
    public interface IDiskService : IDisposable
    {
        /// <summary>
        /// Open data file (creating if doest exists) and validate header
        /// </summary>
        void Initialize(Logger log, string password);

        /// <summary>
        /// Read a page from disk datafile
        /// </summary>
        byte[] ReadPage(uint pageID);

        /// <summary>
        /// Write a page in disk datafile
        /// </summary>
        void WritePage(uint pageID, byte[] buffer);

        /// <summary>
        /// Set datafile length before start writing in disk
        /// </summary>
        void SetLength(long fileSize);

        /// <summary>
        /// Gets file length in bytes
        /// </summary>
        long FileLength { get; }

        /// <summary>
        /// Ensures all pages from the OS cache are persisted on medium
        /// </summary>
        void Flush();

        /// <summary>
        /// 
        /// </summary>
        void Reset();

        /// <summary>
        /// Indicate that disk/instance are data access exclusive (no other process can access)
        /// </summary>
        bool IsExclusive { get; }

        /// <summary>
        /// Read journal file returning IEnumerable of pages
        /// </summary>
        IEnumerable<byte[]> ReadJournal();

        /// <summary>
        /// Write original bytes page in a journal file (in sequence) - if journal not exists, create.
        /// </summary>
        void WriteJournal(ICollection<byte[]> pages);

        /// <summary>
        /// Clear journal file
        /// </summary>
        void ClearJournal();

        /// <summary>
        /// Lock datafile returning lock position
        /// </summary>
        void Lock(LockState state, TimeSpan timeout);

        /// <summary>
        /// Unlock datafile based on last state
        /// </summary>
        void Unlock(LockState state);
    }
}